using DeliveryService.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Infrastructure.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<DeliveryRequest>
    {
        public void Configure(EntityTypeBuilder<DeliveryRequest> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), v)
                )
                .HasDefaultValue(DeliveryStatus.Created);
        }
    }
}