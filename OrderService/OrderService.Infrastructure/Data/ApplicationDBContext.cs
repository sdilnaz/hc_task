using Microsoft.EntityFrameworkCore;
using OrderService.Core.Models;
using OrderService.Infrastructure.Configurations;
using OutboxLibrary.Models;
using OutboxLibrary.Configurations;

namespace OrderService.Infrastructure.Data
{
    public class ApplicationDBContext : DbContext, IOutboxDbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        }
    }
}