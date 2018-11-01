using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            ToTable("OrderItem");
            HasKey(x => x.Id);

            Property(x => x.Price)
                .IsRequired()
                .HasColumnType("money");

            Property(x => x.Quantity)
                .IsRequired();

            HasRequired(x => x.Product);
        }
    }
}
