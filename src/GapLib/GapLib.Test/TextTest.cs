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
                ChatId = ChatId,
                Type = MessageType.Text,
                Data = "hi saied!!!"
            };


            value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_chat_id_deserialized_successfully_from_json()
        {
            ReceivedMessage<string> value = Utils.Deserialize<ReceivedMessage<string>>((Utils.ReadFile(JsonsDirectory + "text\\textreceived.json")));

            ReceivedMessage<string> expected = new ReceivedMessage<string>
            {
                ChatId = ChatId,
                Type = MessageType.Text,
                Data = "hi saied!!!"
            };


            value.Should().BeEquivalentTo(expected);
        }
    }
}
