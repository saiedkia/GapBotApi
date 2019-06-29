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
            GapClient gapClient = new GapClient("token"/*Utils.ReadValue("token", TokenDirectory)*/);
            Message message = new Message()
            {
                Chat_Id = "+989366727432", // invalid chat id
                Data = "salam iran"
            };


            PostResult result = gapClient.Send(message).Result;
            result.StatusCode.Should().Be(StatusCode.InvalidChatIdOrToken);
        }

        [Fact]
        public void Should_send_successfully_and_had_four_button_in_two_row()
        {
            GapClient gapClient = new GapClient(Utils.ReadValue("token", TokenDirectory));
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
                Chat_Id = "+989366727432", 
                Data = "salam iran",
                ReplyKeyboard = keyboard
            };


            PostResult result = gapClient.Send(message).Result;
            result.StatusCode.Should().Be(StatusCode.Success);
        }
    }
}
