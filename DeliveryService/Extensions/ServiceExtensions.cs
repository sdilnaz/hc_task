using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using MassTransit;
using DeliveryService.Consumers;

namespace DeliveryService.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
        }

    }
}