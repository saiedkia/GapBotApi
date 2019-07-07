using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace GapLib
{
    public static class Utils
    {
        public static string ReadValue(string key, string configurationsFilePath = null)
        {
            string path = configurationsFilePath ?? "./Configurations.json";
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
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}
