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
                Chat_Id = "invalid id", // invalid chat id
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
                Chat_Id = ChatId, 
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
            File file = Utils.Deserialize<File>(result.Message);
            file.Should().NotBeNull();
        }

        [Fact]
        public void Upload_and_send_an_image_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            string fileDescription = "coolzila :)";
            PostResult uploadResult = gapClient.Upload(FilesDirectory + "godzila.jpg", "godzila.jpg", UploadFileType.Image, fileDescription).Result;
            File file = Utils.Deserialize<File>(uploadResult.Message);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.Image)
            {
                Chat_Id = ChatId,
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
            File file = Utils.Deserialize<File>(uploadResult.Message);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.File)
            {
                Chat_Id = ChatId,
                Data = Utils.Serialize(file)
            };

            PostResult postResult = gapClient.Send(message).Result;
            postResult.StatusCode.Should().Be(StatusCode.Success);

        }

        [Fact]
        public void Upload_and_send_an_mp3_to_the_client()
        {
            GapClient gapClient = new GapClient(Token);
            string fileDescription ="FiveFin"; // 4.7MB
            PostResult uploadResult = gapClient.Upload(FilesDirectory + "FiveF.mp3", "haghighat_audio.mp3", UploadFileType.Audio, fileDescription).Result;
            File file = Utils.Deserialize<File>(uploadResult.Message);
            file.Desc = fileDescription;
            Message message = new Message(MessageType.Audio)
            {
                Chat_Id = ChatId,
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
                Chat_Id = ChatId,
                Data = "salam donya"
            };

            PostResult postResult = gapClient.Send(message).Result;
            MessageId messageId = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageId>(postResult.Message);

            PostResult deleteResult = gapClient.Delete(ChatId, messageId.id).Result;

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

            PostResult deleteResult = gapClient.SendAction(ChatId).Result;

            deleteResult.StatusCode.Should().Be(200);
        }

    }
}
