using WeCare.Application.Common.Models;

namespace WeCare.Application.Common.Interfaces;
public interface IEmailService
{

    Task SendEmail(EmailMessage message);
}
