using System.IO;

namespace GapLib.Test
{
    public class BaseTest
    {
        public string BaseDirectory => Directory.GetCurrentDirectory() + "\\jsons\\";
        public string TokenDirectory => Directory.GetCurrentDirectory() + "\\TestConfigurations.json";
    }
}
