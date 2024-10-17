using Microsoft.EntityFrameworkCore;
using DeliveryService.Core.Models;
using DeliveryService.Infrastructure.Configurations;

namespace DeliveryService.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<DeliveryRequest> DeliveryRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
        }
    }
}