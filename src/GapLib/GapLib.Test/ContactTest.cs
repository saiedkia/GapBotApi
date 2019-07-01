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
    }
}
