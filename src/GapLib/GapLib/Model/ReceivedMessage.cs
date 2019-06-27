using Newtonsoft.Json.Linq;

namespace GapLib.Model
{
    public class ReceivedMessage
    {
        public long Chat_Id { get; set; }
        public MessageType Type { get; set; }
        public Form Form { get; set; }
        public From From { get; set; }
        public JRaw Data { get; set; }

        public object GetData()
        {
            switch (Type)
            {
                case MessageType.Join:
                case MessageType.Leave:
                case MessageType.Text:
                    return Data.ToString();

                case MessageType.Image:
                case MessageType.Audio:
                case MessageType.Video:
                case MessageType.Voice:
                case MessageType.File:
                    return Utils.Deserialize<File>(Data.Value.ToString());

                case MessageType.Contact:
                    break;
                case MessageType.Location:
                    break;
                case MessageType.SubmitForm:
                    break;
                case MessageType.TriggerButton:
                    break;
                case MessageType.PayCallback:
                    break;
                case MessageType.InvoiceCallback:
                    break;
            }


            return null;
        }

        public static ReceivedMessage Parse(string receivedMessage)
        {
            return Utils.Deserialize<ReceivedMessage>(receivedMessage);
        }
    }
}
