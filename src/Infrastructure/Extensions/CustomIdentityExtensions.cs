using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Identity;
public static class CustomIdentityExtensions
{
    public static IServiceCollection AddCustomIdentityOptions(this IServiceCollection services, Action<CustomIdentityOptions> action)
    {
        services.Configure<CustomIdentityOptions>(action);
        return services;
    }
}
