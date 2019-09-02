using GapLib.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
            try
            {
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ContractResolver = new GapContractResolver()
                };

                return JsonConvert.DeserializeObject<T>(json, setting);
            }

            catch( Exception exp)
            {
                return default(T);
            }
        }

        public static T DeserializeWithDefaultContract<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            catch (Exception exp)
            {
                return default(T);
            }
        }

        public static string Serialize(object obj)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ContractResolver = new GapContractResolver()
            };

            return JsonConvert.SerializeObject(obj, setting);
        }
    }
}
