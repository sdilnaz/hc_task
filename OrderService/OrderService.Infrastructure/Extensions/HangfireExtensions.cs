using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OutboxLibrary.Interfaces;

namespace OrderService.Infrastructure.Extensions
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(
                    options =>options.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
            });

            services.AddHangfireServer();
            return services;
        }

        public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
        {
            app.Services
                .GetRequiredService<IRecurringJobManager>()
                .AddOrUpdate<IOutboxService>(
                    "outbox-processor",
                    job => job.ProcessOutboxMessagesAsync(CancellationToken.None),
                    Cron.Minutely);
            return app;
        }
    }
}