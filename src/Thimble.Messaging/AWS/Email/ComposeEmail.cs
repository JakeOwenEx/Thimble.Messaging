using System.Collections.Generic;
using Amazon.SimpleEmail.Model;

namespace Thimble.Messaging.AWS.Email
{
    public static class ComposeEmail
    {
        public static SendEmailRequest Create(string source, string email)
        {
            var oDestination = new Destination();
            oDestination.ToAddresses = new List<string>(){email};
            
            var subject = new Content();
            subject.Data = "Thanks for signing up";
            
            var textBody = new Content();
            textBody.Data = "Add details/content etc";
            var body = new Body();
            body.Text = textBody;
            
            var message = new Message();
            message.Subject = subject;
            message.Body = body;

            var request = new SendEmailRequest();
            request.Source = source;
            request.Destination = oDestination;
            request.Message = message;

            return request;
        }
    }
}