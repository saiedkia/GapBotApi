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
            //ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "text\\textreceived.json"));
            string message = Utils.ReadFile(JsonsDirectory + "text\\textreceived.json");
            ReceivedMessage value = Utils.Deserialize<ReceivedMessage>(message);

            ReceivedMessage<string> expected = new ReceivedMessage<string>
            {
                ChatId = "123123",
                Type = MessageType.Text,
                Data = "hi saied!!!"
            };


            value.Should().BeEquivalentTo(expected);
        }

        //[Fact]
        //public void Should_convert_FromFormRecevedMessage_to_RecevidMessage_data_type_as_string()
        //{
        //    FromFormReceivedMessage receivedMessage = new FromFormReceivedMessage()
        //    {
        //        ChatId = "123123",
        //        Data = "salam",
        //        Type = MessageType.Text
        //    };

        //    ReceivedMessage<string> message = (ReceivedMessage<string>)receivedMessage;

        //    ReceivedMessage<string> expected = new ReceivedMessage<string>()
        //    {
        //        Data = "salam",
        //        Type = MessageType.Text,
        //        ChatId = "123123",
        //    };


        //    message.Should().BeEquivalentTo(expected);
        //}



        //[Fact]
        //public void Should_convert_FromFormRecevedMessage_to_RecevidMessage_data_type_as_int()
        //{
        //    FromFormReceivedMessage receivedMessage = new FromFormReceivedMessage()
        //    {
        //        ChatId = "123123",
        //        Data = "123123",
        //        Type = MessageType.Text
        //    };

        //    ReceivedMessage<int> message = (ReceivedMessage<int>)receivedMessage;

        //    ReceivedMessage<int> expected = new ReceivedMessage<int>()
        //    {
        //        Data = 123123,
        //        Type = MessageType.Text,
        //        ChatId = "123123",
        //    };


        //    message.Should().BeEquivalentTo(expected);
        //}

        [Fact]
        public void Should_chat_id_deserialized_successfully_from_json()
        {
            ReceivedMessage<string> value = Utils.Deserialize<ReceivedMessage<string>>((Utils.ReadFile(JsonsDirectory + "text\\textreceived.json")));

            ReceivedMessage<string> expected = new ReceivedMessage<string>
            {
                ChatId = "123123",
                Type = MessageType.Text,
                Data = "hi saied!!!"
            };


            value.Should().BeEquivalentTo(expected);
        }


        //[Fact]
        //public void Should_chat_id_deserialized_successfully_in_FromFormReceived()
        //{
        //    FromFormReceivedMessage value = Utils.Deserialize<FromFormReceivedMessage>((Utils.ReadFile(JsonsDirectory + "text\\textreceived.json")));

        //    ReceivedMessage<string> expected = new ReceivedMessage<string>
        //    {
        //        ChatId = "123123",
        //        Type = MessageType.Text,
        //        Data = "hi saied!!!"
        //    };


        //    value.Should().BeEquivalentTo(expected);
        //}
    }
}
