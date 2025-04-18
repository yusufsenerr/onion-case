using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.User
{
    public class UserConfigurations : IEntityTypeConfiguration<Domain.Identity.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Identity.User> builder)
        {
            
            builder
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
