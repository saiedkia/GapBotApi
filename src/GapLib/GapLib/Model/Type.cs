using System;

namespace GapLib.Model
{
    public enum MessageType
    {
        Join,
        Leave,
        Text,
        Image,
        Audio,
        Video,
        Voice,
        File,
        Contact,
        Location,
        SubmitForm,
        TriggerButton,
        PayCallback,
        InvoiceCallback
    }


    public static class MessageTypeExtension
    {
        public static Type GetMessageType(this MessageType msgType)
        {
            switch (msgType)
            {
                case MessageType.Join:
                    break;
                case MessageType.Leave:
                    break;
                case MessageType.Text:
                    break;
                case MessageType.Image:
                    break;
                case MessageType.Audio:
                    break;
                case MessageType.Video:
                    break;
                case MessageType.Voice:
                    break;
                case MessageType.File:
                    break;
                case MessageType.Contact:
                    return typeof(Contact);
                case MessageType.Location:
                    return typeof(Location);
                case MessageType.SubmitForm:
                    return typeof(FormResult);
                case MessageType.TriggerButton:
                    break;
                case MessageType.PayCallback:
                    break;
                case MessageType.InvoiceCallback:
                    break;
                default:
                    break;
            }


            return typeof(string);
        }
    }
}
