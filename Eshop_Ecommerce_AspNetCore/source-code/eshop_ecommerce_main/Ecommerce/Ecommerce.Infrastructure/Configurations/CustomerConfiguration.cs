using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(prop => prop.FullName)
                .HasMaxLength(256);

            builder.Property(prop => prop.Phone)
                .HasMaxLength(20);

            builder.Property(prop => prop.Email)
                .HasMaxLength(256);

            builder.Property(prop => prop.ShippingAddress)
                .HasMaxLength(500);

            builder.Property(prop => prop.BillingAddress)
                .HasMaxLength(500);

            builder.Property(prop => prop.ApplicationUserId)
                            .HasMaxLength(450)
                            .IsRequired();

            builder.HasIndex(prop => prop.ApplicationUserId)
                .IsUnique();

            //builder.HasOne(prop => prop.ApplicationUser)
            //    .WithOne(prop => prop.Customer)
            //    .HasForeignKey<Customer>(prop => prop.ApplicationUserId);

        }
    }
}
