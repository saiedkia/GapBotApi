using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GapLib.Converters
{
    public class ReplyKeyboardConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ReplyKeyboardItem);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ReplyKeyboardItem replyKeyboard = (ReplyKeyboardItem)value;
            JObject jsonObject = new JObject
            {
                { replyKeyboard.Key, replyKeyboard.Value }
            };

            jsonObject.WriteTo(writer);
            writer.Flush();
        }
    }
}
