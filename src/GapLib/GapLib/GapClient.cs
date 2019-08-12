using GapLib.Model;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GapLib
{
    public class GapClient
    {
        const string baseUrl = "https://api.gap.im/";
        //const string editMessage = "https://api.gap.im/editMessage";

        public string Token { get; private set; }
        HttpClient _client = new HttpClient();


        public GapClient(string token)
        {
            Token = token;
            _client.DefaultRequestHeaders.Add("token", Token);
        }

        public async Task<PostResult> Send(Message message)
        {
            HttpResponseMessage response = await _client.PostAsync(baseUrl + "sendMessage", MakeUrl(message));

            PostResult postResult = await GetResult(response);
            return postResult;
        }

        public async Task<PostResult> Upload(string filePath, string fileName, UploadFileType uploadFileType, string fileDescription = null)
        {
            MultipartFormDataContent multipart = new MultipartFormDataContent();
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            multipart.Add(new StreamContent(stream), uploadFileType.ToString().ToLower(), fileName);
            if (fileDescription != null)
                multipart.Add(new StringContent(fileDescription), "desc");
            _client.DefaultRequestHeaders.Add("desc", fileDescription);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "upload", multipart);
            PostResult postResult = await GetResult(response);

            return postResult;
        }

        public async Task<PostResult> Delete(string chatId, string messageId)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", chatId),
                new KeyValuePair<string, string>("message_id", messageId)
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "deleteMessage", content);

            PostResult postResult = await GetResult(response);

            return postResult;
        }

        public async Task<PostResult> SendTypingAction(string chatId)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", chatId),
                new KeyValuePair<string, string>("type", ActionType.Typing.ToString())
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "sendAction", content);

            PostResult postResult = await GetResult(response);

            return postResult;
        }

        public async Task<PostResult> Invoice(Invoice invoice)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.ChatId.ToString()),
                new KeyValuePair<string, string>("amount", invoice.Amount.ToString()),
                new KeyValuePair<string, string>("currency", invoice.Currency.ToString()),
                new KeyValuePair<string, string>("description", invoice.Description)
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "invoice", content);
            PostResult postResult = await GetResult(response);

            return postResult;
        }

        public async Task<PostResult<InvoiceVerficationResult>> InvoiceVerification(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.ChatId),
                new KeyValuePair<string, string>("ref_id", invoice.RefId),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "invoice/verify", content);

            PostResult <InvoiceVerficationResult> postResult = await GetResult<InvoiceVerficationResult>(response);

            return postResult;
        }

        public async Task<PostResult<InvoiceVerficationResult>> PaymentVerification(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.ChatId),
                new KeyValuePair<string, string>("ref_id", invoice.RefId),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = await _client.PostAsync(baseUrl + "payment/verify", content);

            PostResult <InvoiceVerficationResult> postResult = await GetResult<InvoiceVerficationResult>(response);

            return postResult;
        }

        private static async Task<PostResult<T>> GetResult<T>(HttpResponseMessage response)
        {
            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult<T> postResult = new PostResult<T>();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    postResult = new PostResult<T>(StatusCode.Genereic, strResponse);
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    postResult = new PostResult<T>(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number");
            }
            else
                postResult = new PostResult<T>(StatusCode.Success, strResponse);
            return postResult;
        }

        private static async Task<PostResult> GetResult(HttpResponseMessage response)
        {
            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult postResult = new PostResult();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    postResult = new PostResult(StatusCode.Genereic, strResponse);
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    postResult = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number");
            }
            else
                postResult = new PostResult(StatusCode.Success, strResponse);
            return postResult;
        }



        private static FormUrlEncodedContent MakeUrl(Message message)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", message.ChatId),
                new KeyValuePair<string, string>("type", message.Type.ToString()),
                new KeyValuePair<string, string>("data", message.Data)
            };

            if (message.ReplyKeyboard != null)
                values.Add(new KeyValuePair<string, string>("reply_keyboard", Utils.Serialize(message.ReplyKeyboard)));

            if (message.InlineKeyboard != null)
                values.Add(new KeyValuePair<string, string>("inline_keyboard", Utils.Serialize(message.InlineKeyboard)));

            if (message.Form != null)
                values.Add(new KeyValuePair<string, string>("form", Utils.Serialize(message.Form)));

            return new FormUrlEncodedContent(values);
        }
    }
}
