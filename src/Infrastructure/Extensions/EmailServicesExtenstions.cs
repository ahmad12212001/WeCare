using Microsoft.Extensions.DependencyInjection;
using WeCare.Application.Common.Interfaces;
using WeCare.Infrastructure.Configurations;
using WeCare.Infrastructure.Services;

namespace WeCare.Infrastructure.Extensions;
public static class EmailServicesExtenstions
{

    public static IServiceCollection AddEmailService(this IServiceCollection services, Action<EmailConfiguration> options)
    {
        var configurations = new EmailConfiguration();
        options(configurations);

        services.AddSingleton<IEmailService>(o =>
        {
            return new EmailService(configurations);
        });

        return services;
    }
}
