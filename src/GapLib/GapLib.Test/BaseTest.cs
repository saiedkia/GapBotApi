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
}
