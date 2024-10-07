using Microsoft.EntityFrameworkCore;
using OrderService.Data;

namespace OrderService.Extensions
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