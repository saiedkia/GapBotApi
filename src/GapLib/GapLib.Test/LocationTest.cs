using FluentAssertions;
using GapLib.Model;
using Xunit;

namespace GapLib.Test
{
    public class LocationTest : BaseTest
    {
        [Fact]
        public void Read_Location_information()
        {
            ReceivedMessage<Location> receivedMessage = (ReceivedMessage<Location>)ReceivedMessage.Parse(Utils.ReadFile(BaseDirectory + "file\\locationreceived.json"));

            Location expectedLocation = new Location()
            {
                Lat = "36.297611661967245",
                Long = "59.602204039692886",
                Desc = "des"
            };


            receivedMessage.Data.Should().BeEquivalentTo(expectedLocation);
        }
    }
}
