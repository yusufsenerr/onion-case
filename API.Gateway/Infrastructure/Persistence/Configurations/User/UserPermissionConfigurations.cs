using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.User
{
    public class UserPermissionConfigurations : IEntityTypeConfiguration<Domain.Identity.UserPermission>
    {
        public void Configure(EntityTypeBuilder<Domain.Identity.UserPermission> builder)
        {
            builder
                 .HasKey(gp => new { gp.UserId, gp.PermissionId });

            builder
                .HasOne(gp => gp.User)
                .WithMany(g => g.UserPermissions)
                .HasForeignKey(gp => gp.UserId);

            builder
                .HasOne(gp => gp.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(gp => gp.PermissionId);
        }
    }
}
