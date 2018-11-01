using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customer");
            HasKey(x => x.Id);

            Property(x => x.Name.FirstName)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("FirstName");

            Property(x => x.Name.LastName)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnName("LastName");

            Property(x => x.BirthDate);

            Property(x => x.Email.EmailAddress)
                .IsRequired()
                .HasMaxLength(140)
                .HasColumnName("Email");

            Property(x => x.Document.Number)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("Document");

            HasRequired(x => x.User);

        }
    }
}
