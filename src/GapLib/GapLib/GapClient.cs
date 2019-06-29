using GapLib.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GapLib
{
    public class GapClient
    {
        readonly string baseUrl = "https://api.gap.im/sendMessage";
        HttpClient _client = new HttpClient();

        public string Token { get; set; }

        public GapClient(string token)
        {
            Token = token;
            _client.DefaultRequestHeaders.Add("token", Token);
        }

        public async Task<PostResult> Send(Message message)
        {
            HttpResponseMessage response = await _client.PostAsync(baseUrl, MakeUrl(message));

            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult result = new PostResult();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse);
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number");
            }
            else
                result = new PostResult(StatusCode.Success, strResponse);

            return result;
        }



        private FormUrlEncodedContent MakeUrl(Message message)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("chat_id", message.Chat_Id));
            values.Add(new KeyValuePair<string, string>("type", message.Type.ToString()));
            values.Add(new KeyValuePair<string, string>("data", message.Data));
            if (message.ReplyKeyboard != null)
                values.Add(new KeyValuePair<string, string>("reply_keyboard", Utils.Serialize(message.ReplyKeyboard)));

            return new FormUrlEncodedContent(values);
        }
    }
}
