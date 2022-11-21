using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string toEmail, string subject, string message, List<IFormFile> attachments = null);
    }
}
