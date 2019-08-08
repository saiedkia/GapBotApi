using GapLib.Converters;
using Newtonsoft.Json;

namespace GapLib.Model
{
    [JsonConverter(typeof(ReceivedMessageConverter))]
    public class ReceivedMessage : MessageBase
    {
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

    //[JsonConverter(typeof(ReceivedMessageGenericConverter))]
    public class ReceivedMessage<T> : ReceivedMessage
    {

        public T Data { get; set; }

        public ReceivedMessage() { }

        public ReceivedMessage(ReceivedMessage message)
        {
            ChatId = message.ChatId;
            From = message.From;
            Type = message.Type;

        }

        public ReceivedMessage<T> SetData(T data)
        {
            Data = data;
            return this;
        }


        //public static explicit operator ReceivedMessage<T>(FromFormReceivedMessage message)
        //{
        //    ReceivedMessage<T> tmpMessage = new ReceivedMessage<T>();
        //    tmpMessage.ChatId = message.ChatId;
        //    tmpMessage.From = message.From;
        //    tmpMessage.Type = message.Type;
        //    if (!string.IsNullOrEmpty(message.Data))
        //        if (message.Data.Trim().StartsWith("{") && message.Data.Trim().EndsWith("}"))
        //            tmpMessage.Data = Utils.Deserialize<T>(message.Data);
        //        else
        //            tmpMessage.Data = (T)Convert.ChangeType(message.Data, typeof(T));


        //    return tmpMessage;
        //}
    }


}
