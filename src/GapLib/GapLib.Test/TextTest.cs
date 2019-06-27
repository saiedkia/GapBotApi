using FluentAssertions;
using GapLib.Model;
using Newtonsoft.Json.Linq;
using Xunit;

namespace GapLib.Test
{
    public class TextTest : BaseTest
    {
        [Fact]
        public void Should_message_equal_hi_saied()
        {
            ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(BaseDirectory + "text\\textreceived.json"));

            ReceivedMessage expected = new ReceivedMessage
            {
                Chat_Id = 123123,
                Type = MessageType.Join,
                Data = new JRaw("hi saied!!!")
            };


            value.Data.Value.Should().Equals(expected.Data.Value);
        }
    }
}
