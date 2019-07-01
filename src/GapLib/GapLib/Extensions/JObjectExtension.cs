using Newtonsoft.Json.Linq;

namespace GapLib.Extensions
{
    public static class JObjectExtension
    {
        public static JObject AddProperty(this JObject obj, string key, string value)
        {
            obj.Add(key, value);
            return obj;
        }
    }
}
