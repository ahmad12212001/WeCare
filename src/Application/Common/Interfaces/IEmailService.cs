using WeCare.Application.Common.Models;

namespace WeCare.Application.Common.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(EmailMessage message);
    Task SendEmailHtmlAsync(EmailMessage message);
}
