namespace GapLib.Model
{
    public abstract class Message
    {
        public string Chat_Id { get; set; }
        public MessageType Type { get; protected set; } = MessageType.Text;
        public virtual string Data { get; set; }
        public Form Form { get; set; }
        public ReplyKeyboard ReplyKeyboard { get; set; }
        public InlineKeyboard InlineKeyboard { get; set; }
    }

    public class TextMessage : Message
    {

    }
}
