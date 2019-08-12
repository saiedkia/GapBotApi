using FluentAssertions;
using GapLib.Model;
using Xunit;

namespace GapLib.Test
{
    public class JointLeaveReceivedTest : BaseTest
    {
        [Fact]
        public void Should_deserialize_and_get_currect_object_join()
        {
            ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "JoinLeave\\JoinReceived.json"));
            ReceivedMessage expected = new ReceivedMessage
            {
                ChatId = "123123",
                Type = MessageType.Join
            };

            value.Should().BeEquivalentTo(expected); 
        }

        [Fact]
        public void Should_deserialize_and_get_currect_object_leave()
        {
            ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "JoinLeave\\LeaveReceived.json"));
            ReceivedMessage expected = new ReceivedMessage
            {
                ChatId = "123123",
                Type = MessageType.Leave
            };

            value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Should_deserialize_and_get_currect_object_join_with_from_sender_detail()
        {
            ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "JoinLeave\\JoinReceived_withFrom.json"));
            ReceivedMessage expected = new ReceivedMessage
            {
                ChatId = "123123",
                Type = MessageType.Join,
                From = new From()
                {
                    Id = 4455,
                    Username = "userName",
                    Name = "saiedkia"
                }
            };

            value.Should().BeEquivalentTo(expected);
        }
    }
}
