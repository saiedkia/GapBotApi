using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace GapLib.Model
{
    public class File
    {
        public Dictionary<string, string> Screenshots { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageType Type { get; set; }
        public Dictionary<string, string> Tags { get; set; }
        public string Path { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public double? Duration { get; set; } = 0;
        public long? Filesize { get; set; }
        public string Filename { get; set; }
        public string Wavebytes { get; set; }
        public string Desc { get; set; }
    }
}
