using GapLib.Model;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GapLib
{
    public class GapClient
    {
        const string baseUrl = "https://api.gap.im/sendMessage";
        const string deleteUrl = "https://api.gap.im/deleteMessage";
        const string uploadUrl = "https://api.gap.im/upload";
        const string sendAction = "https://api.gap.im/sendAction";
        const string editMessage = "https://api.gap.im/editMessage";
        const string invoiceUrl = "https://api.gap.im/invoice";
        const string invoiceVerificationUrl = "https://api.gap.im/invoice/verify";
        const string invoiceInquiry = "https://api.gap.im/invoice/inquiry";
        const string payementVerify = "https://api.gap.im/payment/verify";
        const string payementInquiry = "https://api.gap.im/payment/inquiry";

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
                else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
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

        public async Task<PostResult> SendAction(string chat_id)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", chat_id),
                new KeyValuePair<string, string>("type", ActionType.Typing.ToString())
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(sendAction, content);

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

        //public Task<PostResult> EditMessage(string messageId, Message message)
        //{

        //}


        public async Task<PostResult> Invoice(Invoice invoice)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.chat_id.ToString()),
                new KeyValuePair<string, string>("amount", invoice.amount.ToString()),
                new KeyValuePair<string, string>("currency", invoice.currency.ToString()),
                new KeyValuePair<string, string>("description", invoice.description)
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(invoiceUrl, content);

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

        public async Task<PostResult<InvoiceVerficationResult>> InvoiceVerification(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.chat_id),
                new KeyValuePair<string, string>("ref_id", invoice.ref_id),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(invoiceVerificationUrl, content);

            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult<InvoiceVerficationResult> result  = new PostResult<InvoiceVerficationResult>();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse).ToType<InvoiceVerficationResult>();
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number").ToType<InvoiceVerficationResult>();
            }
            else
                result = new PostResult(StatusCode.Success, strResponse).ToType<InvoiceVerficationResult>();

            return result;
        }

        public async Task<PostResult<InvoiceVerficationResult>> InvoiceInquiry(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.chat_id),
                new KeyValuePair<string, string>("ref_id", invoice.ref_id),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(invoiceInquiry, content);

            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult<InvoiceVerficationResult> result = new PostResult<InvoiceVerficationResult>();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse).ToType<InvoiceVerficationResult>();
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number").ToType<InvoiceVerficationResult>();
            }
            else
                result = new PostResult(StatusCode.Success, strResponse).ToType<InvoiceVerficationResult>();

            return result;
        }

        public async Task<PostResult<InvoiceVerficationResult>> PaymentVerification(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.chat_id),
                new KeyValuePair<string, string>("ref_id", invoice.ref_id),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(payementVerify, content);

            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult<InvoiceVerficationResult> result = new PostResult<InvoiceVerficationResult>();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse).ToType<InvoiceVerficationResult>();
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number").ToType<InvoiceVerficationResult>();
            }
            else
                result = new PostResult(StatusCode.Success, strResponse).ToType<InvoiceVerficationResult>();

            return result;
        }

        public async Task<PostResult<InvoiceVerficationResult>> PaymentInquiry(InvoiceVerfication invoice)
        {
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("chat_id", invoice.chat_id),
                new KeyValuePair<string, string>("ref_id", invoice.ref_id),
            };

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);

            HttpResponseMessage response = await _client.PostAsync(payementInquiry, content);

            string strResponse = await response.Content.ReadAsStringAsync();
            PostResult<InvoiceVerficationResult> result = new PostResult<InvoiceVerficationResult>();
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    result = new PostResult(StatusCode.Genereic, strResponse).ToType<InvoiceVerficationResult>();
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    result = new PostResult(StatusCode.InvalidChatIdOrToken, "invalid chat_id or phone number").ToType<InvoiceVerficationResult>();
            }
            else
                result = new PostResult(StatusCode.Success, strResponse).ToType<InvoiceVerficationResult>();

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

            if (message.InlineKeyboard != null)
                values.Add(new KeyValuePair<string, string>("inline_keyboard", Utils.Serialize(message.InlineKeyboard)));

            if(message.Form != null)
                values.Add(new KeyValuePair<string, string>("form", Utils.Serialize(message.Form)));

            return new FormUrlEncodedContent(values);
        }


    }
}
