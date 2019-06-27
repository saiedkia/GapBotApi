using FluentAssertions;
using GapLib.Model;
using System.Collections.Generic;
using Xunit;

namespace GapLib.Test
{
    public class ImageTest : BaseTest
    {
        [Fact]
        public void Should_getdata_could_deserialize_data()
        {
            ReceivedMessage value = ReceivedMessage.Parse(Utils.ReadFile(BaseDirectory + "file\\imagereceived.json"));
            Dictionary<string, string> screenshots = new Dictionary<string, string>
            {
                { "64", "sc1" },
                { "128", "sc2" },
                { "256", "sc3" },
                { "512", "sc4" }
            };

            File expectedImageFile = new File()
            {
                ScreenShots = screenshots,
                Type = MessageType.Image,
                Path = "filePath",
                Width = 512,
                Height = 512,
                Filesize = 34376,
                Filename = "image.jpeg",
                Desc = "cap"
            };

            expectedImageFile.Should().BeEquivalentTo(value.GetData());
        }
    }
}
