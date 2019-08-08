using FluentAssertions;
using GapLib.Model;
using System.Collections.Generic;
using Xunit;

namespace GapLib.Test
{
    public class GapClientTest : BaseTest
    {
        [Fact]
        public void Should_get_invalid_token_error_on_send_text_message()
        {
            GapClient gapClient = new GapClient(Utils.ReadValue("token"));
            Message message = new Message()
            {
                ChatId = "invalid id", // invalid chat id
                Data = "salam iran"
            };


            PostResult result = gapClient.Send(message).Result;
            result.StatusCode.Should().Be(StatusCode.InvalidChatIdOrToken);
        }

        [Fact]
        public void Should_send_successfully_and_had_four_button_in_two_row()
        {
            GapClient gapClient = new GapClient(Token);
            ReplyKeyboard keyboard = new ReplyKeyboard();
            keyboard.AddRow(new List<ReplyKeyboardItem>()
            {
                new ReplyKeyboardItem("item1", "value one"),
                new ReplyKeyboardItem("item2", "value two"),
            });

            keyboard.AddRow(new List<ReplyKeyboardItem>()
            {
                new ReplyKeyboardLocationItem("location"),
                new ReplyKeyboardContactItem("contact")
            });

            Message message = new Message
            {
                ChatId = ChatId,
                Data = "salam iran",
                ReplyKeyboard = keyboard
            };


            PostResult result = gapClient.Send(message).Result;
            result.StatusCode.Should().Be(StatusCode.Success);
        }



        [Fact]
        public void Upload_successfully_an_image()
        {
            GapClient gapClient = new GapClient(Token);
            Message msg = new Message(MessageType.Image);
            PostResult result = gapClient.Upload(FilesDirectory + "godzila.jpg", "godzila.jpg", UploadFileType.Image, "coolzila :)").Result;

            result.StatusCode.Should().Be(StatusCode.Success);
            File file = Utils.Deserialize<File>(result.RawBody);
            file.Should().NotBeNull();
        }

        [Fact]
        public void Upload_and_send_an_image_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            string fileDescription = "coolzila :)";
            PostResult uploadResult = gapClient.Upload(FilesDirectory + "godzila.jpg", "godzila.jpg", UploadFileType.Image, fileDescription).Result;
            File file = Utils.Deserialize<File>(uploadResult.RawBody);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.Image)
            {
                ChatId = ChatId,
                Data = Utils.Serialize(file)
            };

            PostResult postResult = gapClient.Send(message).Result;
            postResult.StatusCode.Should().Be(StatusCode.Success);

        }


        [Fact]
        public void Upload_and_send_an_textFile_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            string fileDescription = "some desc";
            PostResult uploadResult = gapClient.Upload(FilesDirectory + "sampleText.txt", "textfile.txt", UploadFileType.File, fileDescription).Result;
            File file = Utils.Deserialize<File>(uploadResult.RawBody);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.File)
            {
                ChatId = ChatId,
                Data = Utils.Serialize(file)
            };

            PostResult postResult = gapClient.Send(message).Result;
            postResult.StatusCode.Should().Be(StatusCode.Success);

        }

        [Fact]
        public void Upload_and_send_an_mp3_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            string fileDescription = "FiveFin"; // 4.7MB
            PostResult uploadResult = gapClient.Upload(FilesDirectory + "FiveF.mp3", "haghighat_audio.mp3", UploadFileType.Audio, fileDescription).Result;
            File file = Utils.Deserialize<File>(uploadResult.RawBody);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.Audio)
            {
                ChatId = ChatId,
                Data = Utils.Serialize(file)
            };

            PostResult postResult = gapClient.Send(message).Result;
            postResult.StatusCode.Should().Be(StatusCode.Success);

        }


        [Fact]
        public void Delete_message()
        {
            GapClient gapClient = new GapClient(Token);

            Message message = new Message
            {
                ChatId = ChatId,
                Data = "salam donya"
            };

            PostResult postResult = gapClient.Send(message).Result;
            MessageId messageId = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageId>(postResult.RawBody);

            PostResult deleteResult = gapClient.Delete(ChatId, messageId.Id).Result;

            deleteResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Delete_should_be_successfull_with_invalid_messageId()
        {
            GapClient gapClient = new GapClient(Token);

            PostResult deleteResult = gapClient.Delete(ChatId, "555555").Result;

            deleteResult.StatusCode.Should().Be(200);
        }


        // need visual assertion :)
        [Fact]
        public void Send_typing_action()
        {
            GapClient gapClient = new GapClient(Token);

            PostResult postResult = gapClient.SendTypingAction(ChatId).Result;

            postResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Sned_an_invoice_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            Invoice invoice = new Invoice()
            {
                ChatId = ChatId,
                Amount = 20_000,
                Currency = Currency.USD,
                Description = "no comment"
            };

            PostResult invoiceResult = gapClient.Invoice(invoice).Result;

            invoiceResult.StatusCode.Should().Be(200);
            invoiceResult.Id.Should().NotBeNull();
        }


        [Fact]
        public void Should_get_error_on_response_because_invoice_not_payd()
        {
            GapClient gapClient = new GapClient(Token);
            Invoice invoice = new Invoice()
            {
                ChatId = ChatId,
                Amount = 20_000,
                Currency = Currency.USD,
                Description = "no comment"
            };

            PostResult invoiceResult = gapClient.Invoice(invoice).Result;

            InvoiceVerfication invoiceVerfication = new InvoiceVerfication()
            {
                ChatId = ChatId,
                RefId = invoiceResult.Id
            };

            PostResult<InvoiceVerficationResult> verificationResult = gapClient.InvoiceVerification(invoiceVerfication).Result;


            verificationResult.Id.Should().BeNull();
            verificationResult.ErrorMessage.Should().BeNull();
            verificationResult.StatusCode.Should().Be(200);
            verificationResult.Data.Status.Should().Be(InvoiceStatus.Error);
        }


        [Fact]
        public void Should_get_error_on_invoiceInquiry_check_because_invoice_not_payd()
        {
            GapClient gapClient = new GapClient(Token);
            Invoice invoice = new Invoice()
            {
                ChatId = ChatId,
                Amount = 20_000,
                Currency = Currency.USD,
                Description = "no comment"
            };

            PostResult invoiceResult = gapClient.Invoice(invoice).Result;

            InvoiceVerfication invoiceVerfication = new InvoiceVerfication()
            {
                ChatId = ChatId,
                RefId = invoiceResult.Id
            };

            PostResult<InvoiceVerficationResult> verificationResult = gapClient.InvoiceVerification(invoiceVerfication).Result;


            verificationResult.Id.Should().BeNull();
            verificationResult.ErrorMessage.Should().BeNull();
            verificationResult.StatusCode.Should().Be(200);
            verificationResult.Data.Status.Should().Be(InvoiceStatus.Error);
        }

        [Fact]
        public void Client_should_receive_a_form_whit_all_input_types()
        {
            GapClient gapClient = new GapClient(Token);

            Message message = new Message()
            {
                ChatId = ChatId,
                Data = "فرم زیر با پر نمایید"
            };


            FormItem inputForm = new FormItem()
            {
                Label = "name",
                Name = "name",
                Type = FormType.Text
            };

            FormItem checkboxForm = new FormItem()
            {
                Label = "checkbox label",
                Name = "chk",
                Type = FormType.Checkbox
            };

            FormItemOptional listForm = new FormItemOptional()
            {
                Label = "list label",
                Name = "listLbl",
                Type = FormType.Select,
                Options = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("row1", "row 1"),
                    new KeyValuePair<string, string>("row2", "row 2")
                }
            };


            message.Form = new Form
            {
                inputForm,
                checkboxForm,
                listForm
            };


            PostResult result = gapClient.Send(message).Result;

            result.Id.Should().NotBeNull();
            result.ErrorMessage.Should().BeNull();
            result.StatusCode.Should().Be(200);
        }

    }
}
