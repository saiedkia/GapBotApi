using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GapLib
{
    public static class Utils
    {
        public static string ReadValue(string key, string filePath = null)
        {
            string path = filePath ?? Directory.GetCurrentDirectory() + "\\db.json";
            if (File.Exists(path))
            {
                string jsonContent = File.ReadAllText(path);
                JObject jo = JObject.Parse(jsonContent);
                return jo[key].ToString();
            }

            return null;
        }

        public static string ReadFile(string filePath)
        {
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            return null;
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
