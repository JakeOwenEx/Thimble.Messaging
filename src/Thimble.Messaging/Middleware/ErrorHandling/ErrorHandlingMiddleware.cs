using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Thimble.Messaging.Controllers.Exceptions;
using Thimble.Messaging.logging;
using Thimble.Product.Middleware.ErrorHandling.ErrorResponses;

namespace Thimble.Messaging.Middleware.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, IThimbleLogger thimbleLogger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var responseObject = new ErrorResponse { Message = ex.Message };

                if (ex.GetType() == typeof(AuthHeaderRequiredException)) 
                    SetResponse(StaticErrorResponses.AuthHeaderRequired, context, responseObject);
                
                if (ex.GetType() == typeof(InvalidAuthHeaderException)) 
                    SetResponse(StaticErrorResponses.InvalidAuthKey, context, responseObject);

                var serializedBody = JsonConvert.SerializeObject(responseObject, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
                thimbleLogger.Log(ex, "product.error");
                await context.Response.WriteAsync(serializedBody, Encoding.UTF8);
            }
        }

        private void SetResponse(BaseErrorResponse errorResponse, HttpContext context, ErrorResponse responseObject)
        {
            context.Response.StatusCode = errorResponse.StatusCode;
            responseObject.Message = errorResponse.Message;
        }
    }
}