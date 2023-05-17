using System.Reflection;
using Hangfire;
using Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class HangFireJobsExtensions
{
    public static IApplicationBuilder UseRecurringJob(this IApplicationBuilder app, Assembly assembly)
    {
        var assginedType = typeof(IRecurringJob);
        var reccurringJobManager = app.ApplicationServices.GetRequiredService<IRecurringJobManager>();
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceProvider = serviceScope.ServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<IApplicationDbContext>();

            var types = assembly.GetTypes().Where(t => assginedType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToArray();
            string[] jobNames = (types.Select(t => t.Name).OfType<string>().ToArray())!;
            var jobs = dbContext.Jobs.OrderBy(j => j.Order).ToListAsync().GetAwaiter().GetResult();

            foreach (var type in types)
            {

                var interfaces = type.GetInterfaces();
                if (interfaces.Length == 0)
                {
                    continue;
                }

                var job = jobs!.Where(j => j.Name == type.Name).First();

                var classInstance = (IRecurringJob)Activator.CreateInstance(type, serviceProvider)!;

                reccurringJobManager.AddOrUpdate($"{job.Name}-{job.Id}", () => classInstance.Run(), job.CronExpression);

            }

            return app;
        }

    }
}
