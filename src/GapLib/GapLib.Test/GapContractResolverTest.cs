using FluentAssertions;
using Xunit;

namespace GapLib.Test
{
    public class GapContractResolverTest
    {
        public class SerializationModel
        {
            public string ChatId { get; set; }
        }

        [Fact]
        public void Should_add_under_line_between_words()
        {
            SerializationModel model = new SerializationModel() { ChatId = "123" };
            string expected = "chat_id";

            string serialized = Utils.Serialize(model);

            serialized.Should().Contain(expected);
        }


        [Fact]
        public void Should_remove_under_line_when_deserialize_objects()
        {
            string input = "{\"chat_id\":\"123\"}";
            SerializationModel model = new SerializationModel() { ChatId = "123" };

            SerializationModel result = Utils.Deserialize<SerializationModel>(input);

            result.Should().BeEquivalentTo(model);
        }
    }
}
