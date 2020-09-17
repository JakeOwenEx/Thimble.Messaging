using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Mvc;
using Thimble.Messaging.AWS.Email;
using Thimble.Messaging.Controllers.Authorization;
using Thimble.Messaging.Controllers.TraceId;
using Thimble.Messaging.logging;
using Thimble.Messaging.Models;

namespace Thimble.Messaging.Controllers
{
    [Route("messaging/")]
    [ApiController]
    [Authorization]
    [TraceIdResolver]
    public class MessagingController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IThimbleLogger _thimbleLogger;
        public MessagingController(
            IEmailService emailService,
            IThimbleLogger thimbleLogger)
        {
            _emailService = emailService;
            _thimbleLogger = thimbleLogger;
        }

        [HttpPost("sendEmail")]
        public async Task<OkResult> StoreAndSendEmail([FromBody] EmailRequest emailRequest)
        {
            _thimbleLogger.Log("messaging.email.started");
            await _emailService.SendEmail(emailRequest);
            return Ok();
        }
    }
}
