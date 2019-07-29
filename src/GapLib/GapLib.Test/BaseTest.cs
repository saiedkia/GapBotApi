using FluentAssertions.Primitives;
using System.IO;

namespace GapLib.Test
{
    public class BaseTest
    {
        public string JsonsDirectory => Directory.GetCurrentDirectory() + "\\jsons\\";
        public string FilesDirectory => Directory.GetCurrentDirectory() + "\\files\\";
        public string ChatId => Utils.ReadValue("chat_id");
        public string Token => Utils.ReadValue("token");
    }


    public static class StringAssertionExtension
    {
        public static bool EqualInsensitive(this StringAssertions stringAssertions, string expected)
        {
            string tmpExpected = expected.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");
            string tmpVlaue = stringAssertions.Subject.Replace(" ", "").Replace("\n", "").Replace("\t", "").Replace("\r", "");

            return tmpExpected .Equals(tmpVlaue);
        }
    }
}
