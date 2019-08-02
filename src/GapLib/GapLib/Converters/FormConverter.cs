using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace GapLib.Converters
{
    public class FormConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FormItem);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            FormItem formItem = (FormItem)value;
            JObject jsonObject = new JObject();
            foreach (PropertyInfo info in formItem.GetType().GetProperties())
                if (info.GetValue(formItem) == null) continue;
                else if (info.Name.ToLower() == nameof(FormItemOptional.Options).ToLower())
                {
                    jsonObject.Add(info.Name.ToLower(), JToken.FromObject(info.GetValue(formItem)));
                }
                else
                {
                    jsonObject.Add(info.Name.ToLower(), info.GetValue(formItem).ToString());
                }

            jsonObject.WriteTo(writer);
            writer.Flush();
        }
    }
}
