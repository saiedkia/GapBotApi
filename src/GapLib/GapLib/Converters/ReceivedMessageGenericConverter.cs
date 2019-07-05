using GapLib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GapLib.Converters
{
    public class ReceivedMessageGenericConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ReceivedMessageGenericConverter);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            ReceivedMessage message = new ReceivedMessage
            {
                Chat_Id = obj["chat_id"]?.ToObject<string>() ?? "0",
                From = obj["from"]?.ToObject<From>()
            };


            MessageType type = obj["type"]?.ToObject<MessageType>() ?? MessageType.Text;
            message.Type = type;
            switch (type)
            {
                case MessageType.Join:
                case MessageType.Leave:
                case MessageType.Text:
                    return new ReceivedMessage<string>(message).SetData(obj["data"]?.ToObject<string>() ?? null);
                case MessageType.Image:
                case MessageType.Audio:
                case MessageType.Video:
                case MessageType.Voice:
                case MessageType.File:
                    return new ReceivedMessage<File>(message).SetData(obj["data"]?.ToObject<File>() ?? null);
                case MessageType.Contact:
                    return new ReceivedMessage<Contact>(message).SetData(obj["data"]?.ToObject<Contact>() ?? null);
                case MessageType.Location:
                    return new ReceivedMessage<Location>(message).SetData(obj["data"]?.ToObject<Location>() ?? null);
                case MessageType.SubmitForm:
                    return new ReceivedMessage<Form>(message).SetData(obj["data"]?.ToObject<Form>() ?? null);
                case MessageType.TriggerButton:
                    break;
                case MessageType.PayCallback:
                    break;
                case MessageType.InvoiceCallback:
                    break;
                default:
                    break;
            }


            return message;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }

}
