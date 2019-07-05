using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GapLib.Converters
{
    public class ReceivedMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ReceivedMessageGenericConverter);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            ReceivedMessage<string> message = new ReceivedMessage<string>
            {
                Chat_Id = obj["chat_id"]?.ToObject<string>() ?? "0",
                From = obj["from"]?.ToObject<From>()
            };


            MessageType type = obj["type"]?.ToObject<MessageType>() ?? MessageType.Text;
            message.Type = type;
            message.SetData(obj["data"]?.ToObject<string>() ?? null);


            return message;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
