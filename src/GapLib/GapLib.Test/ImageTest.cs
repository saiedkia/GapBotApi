using FluentAssertions;
using GapLib.Model;
using System.Collections.Generic;
using Xunit;

namespace GapLib.Test
{
    public class ImageTest : BaseTest
    {
        [Fact]
        public void Should_can_deserialize_image_data()
        {
            ReceivedMessage<File> value = (ReceivedMessage<File>)ReceivedMessage.Parse(Utils.ReadFile(BaseDirectory + "file\\imagereceived.json"));
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
                Path = "https://filePath",
                Width = 512,
                Height = 512,
                Filesize = 34376,
                Filename = "image.jpeg",
                Desc = "cap"
            };

            ReceivedMessage<File> expectedmessage = new ReceivedMessage<File>()
            {
                Chat_Id = 123123,
                Type = MessageType.Image,
                Data = expectedImageFile
            };


            value.Should().BeEquivalentTo(expectedmessage);
            expectedImageFile.Should().BeEquivalentTo(value.Data);
        }

        [Fact]
        public void Data_sould_be_null()
        {
            ReceivedMessage<File> value = (ReceivedMessage<File>)ReceivedMessage.Parse(Utils.ReadFile(BaseDirectory + "file\\imagereceived_nullData.json"));

            Assert.Null(value.Data);
        }


    }
}
