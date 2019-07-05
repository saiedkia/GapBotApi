using GapLib.Converters;
using Newtonsoft.Json;
using System;

namespace GapLib.Model
{
    [JsonConverter(typeof(ReceivedMessageConverter))]
    public class ReceivedMessage
    {
        public string Chat_Id { get; set; }
        public MessageType Type { get; set; }
        public From From { get; set; }

        public static ReceivedMessage Parse(string receivedMessage)
        {
            return Utils.Deserialize<ReceivedMessage>(receivedMessage);
        }

        public static ReceivedMessage<T> Parse<T>(string receivedMessage)
        {
            return Utils.Deserialize<ReceivedMessage<T>>(receivedMessage);
        }
    }

    public class FromFormReceivedMessage : ReceivedMessage
    {
        public string Data { get; set; }
    }

    [JsonConverter(typeof(ReceivedMessageGenericConverter))]
    public class ReceivedMessage<T> : ReceivedMessage
    {

        public T Data { get; set; }

        public ReceivedMessage() { }

        public ReceivedMessage(ReceivedMessage message)
        {
            Chat_Id = message.Chat_Id;
            From = message.From;
            Type = message.Type;

        }

        public ReceivedMessage<T> SetData(T data)
        {
            Data = data;
            return this;
        }


        public static explicit operator ReceivedMessage<T>(FromFormReceivedMessage message)
        {
            ReceivedMessage<T> tmpMessage = new ReceivedMessage<T>();
            tmpMessage.Chat_Id = message.Chat_Id;
            tmpMessage.From = message.From;
            tmpMessage.Type = message.Type;
            if (!string.IsNullOrEmpty(message.Data))
                if (message.Data.Trim().StartsWith("{") && message.Data.Trim().EndsWith("}"))
                    tmpMessage.Data = Utils.Deserialize<T>(message.Data);
                else
                    tmpMessage.Data = (T)Convert.ChangeType(message.Data, typeof(T));


            return tmpMessage;
        }
    }


}
