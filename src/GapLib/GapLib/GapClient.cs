using GapLib.Model;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GapLib
{
    public class GapClient
    {
        readonly string baseUrl = "https://api.gap.im/sendMessage";
        readonly string deleteUrl = "https://api.gap.im/deleteMessage";
        readonly string uploadUrl = "https://api.gap.im/upload";

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

        public async Task<PostResult> Upload(string filePath, string fileName, UploadFileType uploadFileType, string fileDescription = null)
        {
            MultipartFormDataContent multipart = new MultipartFormDataContent();
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            multipart.Add(new StreamContent(stream), uploadFileType.ToString().ToLower(), fileName);
            if (fileDescription != null)
                multipart.Add(new StringContent(fileDescription), "desc");
            _client.DefaultRequestHeaders.Add("desc", fileDescription);

            HttpResponseMessage response = await _client.PostAsync(uploadUrl, multipart);
            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult result = new PostResult();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse);
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number");
                else if(response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    result = new PostResult(StatusCode.InvalidFileTypeOrSize, "invalid filetype or size");
                    
            }
            else
                result = new PostResult(StatusCode.Success, strResponse);


            return result;
        }

        public async Task<PostResult> Delete(string chat_id, string message_id)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", chat_id),
                new KeyValuePair<string, string>("message_id", message_id)
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(deleteUrl, content);

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
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", message.Chat_Id),
                new KeyValuePair<string, string>("type", message.Type.ToString()),
                new KeyValuePair<string, string>("data", message.Data)
            };

            if (message.ReplyKeyboard != null)
                values.Add(new KeyValuePair<string, string>("reply_keyboard", Utils.Serialize(message.ReplyKeyboard)));

            if(message.InlineKeyboard != null)
                values.Add(new KeyValuePair<string, string>("inline_keyboard", Utils.Serialize(message.InlineKeyboard)));


            return new FormUrlEncodedContent(values);
        }


    }
}
