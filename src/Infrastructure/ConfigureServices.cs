using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.AspNetCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Extensions;
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
            options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
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


        services.AddScoped<IProfileService, CustomProfileService>();
        services.AddIdentityServer().AddProfileService<CustomProfileService>()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();

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

        services.AddMinio(new Uri($"s3://{configuration["Bucket:AccessKey"]}:{configuration["Bucket:SecretKey"]}@{configuration["Bucket:EndPoint"]}/"));

        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}

