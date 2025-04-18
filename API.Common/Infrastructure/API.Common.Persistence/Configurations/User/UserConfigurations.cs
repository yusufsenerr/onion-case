using API.Common.Domain.Balance;
using API.Common.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Common.Persistence.Configurations.User
{
    public class UserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(u => u.UserBalance)
                .WithOne(b => b.AppUser)
                .HasForeignKey<UserBalance>(b => b.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
