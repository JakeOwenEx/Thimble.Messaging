using System.Threading.Tasks;
using Thimble.Messaging.Models;

namespace Thimble.Messaging.AWS.Email
{
    public interface IEmailService
    {
        Task SendEmail(EmailRequest emailRequest);
    }
}