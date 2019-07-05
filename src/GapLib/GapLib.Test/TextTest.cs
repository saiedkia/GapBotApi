using FluentAssertions;
using GapLib.Model;
using Xunit;

namespace GapLib.Test
{
    public class TextTest : BaseTest
    {
        [Fact]
        public void Should_message_equal_hi_saied()
        {
            ReceivedMessage<string> value = (ReceivedMessage<string>)ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "text\\textreceived.json"));

            ReceivedMessage<string> expected = new ReceivedMessage<string>
            {
                Chat_Id = "123123",
                Type = MessageType.Text,
                Data = "hi saied!!!"
            };


            value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_convert_FromFormRecevedMessage_to_RecevidMessage_data_type_as_string()
        {
            FromFormReceivedMessage receivedMessage = new FromFormReceivedMessage()
            {
                Chat_Id = "123123",
                Data = "salam",
                Type = MessageType.Text
            };

            ReceivedMessage<string> message = (ReceivedMessage<string>)receivedMessage;

            ReceivedMessage<string> expected = new ReceivedMessage<string>()
            {
                Data = "salam",
                Type = MessageType.Text,
                Chat_Id = "123123",
            };


            message.Should().BeEquivalentTo(expected);
        }



        [Fact]
        public void Should_convert_FromFormRecevedMessage_to_RecevidMessage_data_type_as_int()
        {
            FromFormReceivedMessage receivedMessage = new FromFormReceivedMessage()
            {
                Chat_Id = "123123",
                Data = "123123",
                Type = MessageType.Text
            };

            ReceivedMessage<int> message = (ReceivedMessage<int>)receivedMessage;

            ReceivedMessage<int> expected = new ReceivedMessage<int>()
            {
                Data = 123123,
                Type = MessageType.Text,
                Chat_Id = "123123",
            };


            message.Should().BeEquivalentTo(expected);
        }
    }
}
