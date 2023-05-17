using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hangfire;

public static class HangFireJobsRegistrar
{

    public static IServiceCollection AddWeCareHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(hangfire =>
        {
            hangfire.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
            hangfire.UseSimpleAssemblyNameTypeSerializer();
            hangfire.UseRecommendedSerializerSettings();
            hangfire.UseColouredConsoleLogProvider();
            hangfire.UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHangfireServer();
        return services;

    }

    public static IApplicationBuilder UseWeCareHangfire(this IApplicationBuilder application)
    {
        application.UseHangfireDashboard();
        application.UseRecurringJob(Assembly.Load("Jobs"));
        return application;
    }
}
