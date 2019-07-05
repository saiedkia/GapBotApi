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
    }
}
