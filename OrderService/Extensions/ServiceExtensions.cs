using Microsoft.EntityFrameworkCore;
using task1.Data;

namespace task1.Extensions
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