using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message, List<IFormFile> attachments);
        Task SendTwoFactorEmailAsync(string username, string subject);
        Task SendResetPasswordEmailAsync(string email, string subject, string url);
        Task SendEmailConfirmationAsync(string email, string subject, string url);
    }
}
