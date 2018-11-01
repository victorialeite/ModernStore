using ModernStore.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ModernStore.Infra.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            ToTable("Order");
            HasKey(x => x.Id);

            Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(8)
                .IsFixedLength();

            Property(x => x.CreateDate);

            Property(x => x.Status);

            Property(x => x.DeliveryFee)
                .HasColumnType("money");

            Property(x => x.Discount)
                .HasColumnType("money");
            
            HasRequired(x => x.Customer);

            HasMany(x => x.Items);
        }
    }
}
