using FluentAssertions;
using Xunit;

namespace GapLib.Test
{
    public class UtilsTest
    {
        [Fact]
        public void Read_token_from_configurations_successfully()
        {
            string result = Utils.ReadValue("token");
            result.Should().NotBeNull();
        }
    }
}
