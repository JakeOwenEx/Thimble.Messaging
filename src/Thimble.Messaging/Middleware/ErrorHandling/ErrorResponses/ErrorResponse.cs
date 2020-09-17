using Newtonsoft.Json;

namespace Thimble.Product.Middleware.ErrorHandling.ErrorResponses
{
    public class ErrorResponse
    {
        [JsonProperty] public string Message { get; set; }
    }
}