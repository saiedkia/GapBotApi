using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace GapLib.Converters
{
    class InlineKeyboardItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(InlineKeyboardItem);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            InlineKeyboardItem replyKeyboard = (InlineKeyboardItem)value;
            JObject jsonObject = new JObject();
            foreach (PropertyInfo info in replyKeyboard.GetType().GetProperties())
                if (info.GetValue(replyKeyboard) == null) continue;
                else
                {
                    jsonObject.Add(GapContractResolver.GetGapNamingConvention(info.Name), info.GetValue(replyKeyboard).ToString());
                }

            jsonObject.WriteTo(writer);
            writer.Flush();
        }
    }
}
