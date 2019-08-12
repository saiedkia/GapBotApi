using FluentAssertions;
using GapLib.Model;
using Xunit;

namespace GapLib.Test
{
    public class ContactTest : BaseTest
    {
        [Fact]
        public void Read_contact_information()
        {
            ReceivedMessage<Contact> receivedMessage = (ReceivedMessage<Contact>)ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "file\\contactreceived.json"));

            Contact expectedContact = new Contact()
            {
                Name = "Ehsan Sabet",
                Phone = "+989356167766"
            };


            receivedMessage.Data.Should().BeEquivalentTo(expectedContact);
        }


        [Fact]
        public void Should_send_an_get_contact_inline_button()
        {
            GapClient gapClient = new GapClient(Token);
            Message message = new Message()
            {
                ChatId = ChatId,
                ReplyKeyboard = ReplyKeyboard.Builder().AddGetContact("share phone").Build(),
                Data = "let me khnow your phone number..."
            };


            PostResult postResult = gapClient.Send(message).Result;
            postResult.StatusCode.Should().Be(StatusCode.Success);
        }


        [Fact]
        public void Should_send_2X2_reply_keyboard()
        {
            GapClient gapClient = new GapClient(Token);
            Message message = new Message()
            {
                ChatId = ChatId,
                ReplyKeyboard = ReplyKeyboard.Builder().AddGetContact("share phone").Add("button two").AddRow().AddRow().AddGetLocation("share location").Add("button four").Build(),
                Data = "send to me your phone or location"
            };


            PostResult postResult = gapClient.Send(message).Result;

            postResult.StatusCode.Should().Be(StatusCode.Success);
        }
    }
}
