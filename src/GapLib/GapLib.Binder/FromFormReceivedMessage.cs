using Microsoft.AspNetCore.Mvc;

namespace GapLib.Mvc
{

    public class FromFormReceivedMessageAttribute : ModelBinderAttribute
    {
        public FromFormReceivedMessageAttribute() : base(typeof(ReceivedMessageBinder))
        {

        }
    }
}
