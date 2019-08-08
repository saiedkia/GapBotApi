using Microsoft.AspNetCore.Mvc;

namespace GapLib.Mvc
{

    public class FromFormReceivedMessage : ModelBinderAttribute
    {
        public FromFormReceivedMessage() :base(typeof(ReceivedMessageBinder))
        {

        }
    }
}
