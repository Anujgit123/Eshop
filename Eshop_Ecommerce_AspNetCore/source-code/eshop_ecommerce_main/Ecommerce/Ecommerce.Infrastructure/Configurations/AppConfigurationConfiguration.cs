using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Sql.Configurations
{
    public class AppConfigurationConfiguration : IEntityTypeConfiguration<AppConfiguration>
    {
        public void Configure(EntityTypeBuilder<AppConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.Property(r => r.Id)
            //    .ValueGeneratedNever();

            builder.Property(prop => prop.Key)
                .HasMaxLength(256);


        }
    }
}