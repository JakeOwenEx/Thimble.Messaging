using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Thimble.Messaging.logging;

namespace Thimble.Messaging.Controllers.TraceId
{
    public class TraceIdResolverAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<IThimbleLogger>();
            logger.SetTraceId(Guid.NewGuid().ToString());
            base.OnActionExecuting(context);
        }
    }
}