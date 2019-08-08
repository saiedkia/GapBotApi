using GapLib.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

//https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-2.2
namespace GapLib.Mvc
{
    public class ReceivedMessageBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var rawBody = bindingContext.HttpContext.Request.Form.GetStringValue();
            var receivedMessage = Utils.Deserialize<ReceivedMessage>(rawBody);
            bindingContext.Result = ModelBindingResult.Success(receivedMessage);

            return Task.CompletedTask;
            }
    }


    public static class IFormCollectionExtension
    {
        public static string GetStringValue(this IFormCollection collection)
        {
            JObject jtoken = new JObject();
            foreach (string key in collection.Keys)
            {
                collection.TryGetValue(key, out StringValues Val);
                jtoken.Add(key, Val[0]);
            }

            return Utils.Serialize(jtoken);
        }
    }
}
