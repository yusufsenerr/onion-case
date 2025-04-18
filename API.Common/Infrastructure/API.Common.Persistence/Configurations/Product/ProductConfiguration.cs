using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Common.Persistence.Configurations.Product
{
    public class ProductConfiguration : IEntityTypeConfiguration<API.Common.Domain.Product.Product>
    {
        public void Configure(EntityTypeBuilder<API.Common.Domain.Product.Product> builder)
        {
            builder.Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }

}
