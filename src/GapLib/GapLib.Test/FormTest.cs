using FluentAssertions;
using GapLib.Model;
using System.Collections.Generic;
using Xunit;

namespace GapLib.Test
{
    public class FormTest : BaseTest
    {
        [Fact]
        public void Read_form_information()
        {
            ReceivedMessage<FormResult> receivedMessage = (ReceivedMessage<FormResult>)ReceivedMessage.Parse(Utils.ReadFile(JsonsDirectory + "form\\FormRecevied.json"));

            FormResult expectedForm = new FormResult()
            {
                Callback_id = "N7YcI5rAlX2sEFmh",
                Message_id = 97,
                Data = "name=Ehsan&married=y&city=mashhad&address=Iran&agree=true"
            };

            receivedMessage.Data.ParseData().Should().BeEquivalentTo(expectedForm.ParseData());
            Assert.Contains(new KeyValuePair<string, string>("name", "Ehsan"), receivedMessage.Data.ParseData());
        }
    }
}
