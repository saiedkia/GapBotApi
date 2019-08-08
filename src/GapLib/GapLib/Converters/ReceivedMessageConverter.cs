using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace GapLib.Converters
{
    public class ReceivedMessageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ReceivedMessageConverter);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            MessageType type = obj["type"]?.ToObject<MessageType>() ?? MessageType.Text;

            ReceivedMessage receivedMessage = (ReceivedMessage)Activator.CreateInstance(typeof(ReceivedMessage<>).MakeGenericType(type.GetMessageType()));

            receivedMessage.ChatId = obj["chat_id"]?.ToString();
            receivedMessage.From = obj["from"]?.ToObject<From>();

            receivedMessage.Type = type;
            MethodInfo methodSetData = receivedMessage.GetType().GetMethod("SetData");

            object data = obj["data"]?.ToObject(type.GetMessageType());
            if (data != null) 
                methodSetData.Invoke(receivedMessage, new object[]{ data});
            //(receivedMessage).SetData(obj["data"]?.ToObject(type.GetMessageType()) ?? null);


            return receivedMessage;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
