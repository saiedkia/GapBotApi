using GapLib.Model;
using GapLib.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace SelfHostService
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public void Post([FromForm, FromFormReceivedMessage] ReceivedMessage receivedMessage)
        {
            
        }
    }
}
