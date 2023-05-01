using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Extensions;
using WeCare.Infrastructure.Files;
using WeCare.Infrastructure.Identity;
using WeCare.Infrastructure.Persistence;
using WeCare.Infrastructure.Persistence.Interceptors;
using WeCare.Infrastructure.Services;

namespace WeCare.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddCustomIdentityOptions(o =>
        {
            o.RequiredActiveAccount = true;
            o.SignIn = new SignInOptions
            {
                RequireConfirmedEmail = true
            };
        });

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

        services.AddSingleton<IPasswordGenerator, PasswordGenerator>();

        services.AddEmailService(options =>
        {
            options.SmtpServer = configuration["EmailConfiguration:SmtpServer"]!;
            options.Port = Convert.ToInt32(configuration["EmailConfiguration:Port"]);
            options.UserName = configuration["EmailConfiguration:UserName"]!;
            options.From = configuration["EmailConfiguration:From"]!;
            options.Password = configuration["EmailConfiguration:Password"]!;
            options.FromName = configuration["EmailConfiguration:FromName"]!;
        });

        services.AddAuthentication()
            .AddIdentityServerJwt();

        services.AddAuthorization(options =>
                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
