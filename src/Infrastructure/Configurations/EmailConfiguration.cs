using Microsoft.Extensions.Options;

namespace WeCare.Infrastructure.Configurations;
public class EmailConfiguration : IOptions<EmailConfiguration>
{
    public string From { get; set; } = null!;
    public string FromName { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;

    public EmailConfiguration Value => this;
}