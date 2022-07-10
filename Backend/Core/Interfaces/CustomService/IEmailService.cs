using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.CustomService
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(User user, string callbackUrl);
        Task SendEmailAsync(string email, string subject, string message);
    }
}
