using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Models.Configurations
{
    public class DeliveryConfiguration : IEntityTypeConfiguration<DeliveryRequest>
    {
        public void Configure(EntityTypeBuilder<DeliveryRequest> builder)
        {
            builder.Property(o => o.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (DeliveryStatus)Enum.Parse(typeof(DeliveryStatus), v)
                )
                .HasDefaultValue(DeliveryStatus.Created);
        }
    }
}