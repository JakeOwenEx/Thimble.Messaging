using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Logging;
using Thimble.Messaging.logging;
using Thimble.Messaging.Models;

namespace Thimble.Messaging.AWS.Email
{
    public class EmailService : IEmailService
    {
        private readonly IAwsService _awsService;
        private readonly IThimbleLogger _thimbleLogger;
        
        public EmailService(
            IAwsService awsService,
            IThimbleLogger thimbleLogger)
        {
            _awsService = awsService;
            _thimbleLogger = thimbleLogger;
        }

        public async Task SendEmail(EmailRequest emailRequest)
        {
            var dynamoClient = _awsService.GetDynamoClient();
            await dynamoClient.PutItemAsync(DynamoConstants.EMAILS_TABLE, new Dictionary<string, AttributeValue>
            {
                {DynamoConstants.UserIdKey, new AttributeValue(emailRequest.UserId)},
                {DynamoConstants.EmailKey, new AttributeValue(emailRequest.Email)},
            });
            _thimbleLogger.Log("messaging.email.stored");
            var sesClient = _awsService.GetSesClient();
            await sesClient.SendEmailAsync(ComposeEmail.Create(DynamoConstants.ThimbleEmail, emailRequest.Email));
            _thimbleLogger.Log("messaging.email.sent");
        }
    }
}