using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.UserBalance
{
    public class UserBalanceConfiguration : IEntityTypeConfiguration<API.Common.Domain.Balance.UserBalance>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<API.Common.Domain.Balance.UserBalance> builder)
        {
            builder.Property(e => e.Amount).HasPrecision(18, 2);
        }
    }
}
