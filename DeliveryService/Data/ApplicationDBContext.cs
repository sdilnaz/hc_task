using Microsoft.EntityFrameworkCore;
using DeliveryService.Models;
using DeliveryService.Models.Configurations;

namespace DeliveryService.Data
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