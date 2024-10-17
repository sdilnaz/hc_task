using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Extensions
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