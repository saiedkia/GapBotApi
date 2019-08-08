namespace GapLib.Model
{
    public class Message : MessageBase
    {
        public MessageType Type { get; private set; }
        public string Data { get; set; }
        public Form Form { get; set; }
        public ReplyKeyboard ReplyKeyboard { get; set; }
        public InlineKeyboard InlineKeyboard { get; set; }

        public Message(MessageType messageType = MessageType.Text)
        {
            Type = messageType;
        }

        public Message(ReceivedMessage receivedMessage)
        {
            ChatId = receivedMessage.ChatId;
            Type = receivedMessage.Type;
        }
    }
}
