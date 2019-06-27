using System.Collections.Generic;

namespace GapLib.Model
{
    public class File
    {
        public Dictionary<string, string> ScreenShots { get; set; }
        public MessageType Type { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public string Path { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Duration { get; set; } = 0;
        public long Filesize { get; set; }
        public string Filename { get; set; }
        public string Desc { get; set; }
    }


    //public class ScreenShot
    //{
    //    [JsonProperty("64")]
    //    public string Size_64 { get; set; }

    //    [JsonProperty("128")]
    //    public string Size_128 { get; set; }

    //    [JsonProperty("256")]
    //    public string Size_256 { get; set; }

    //    [JsonProperty("512")]
    //    public string Size_512 { get; set; }
    //}
}
