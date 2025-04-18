using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Permission
{
    public class GroupPermission : IEntityTypeConfiguration<Domain.Identity.GroupPermission>
    {
        public void Configure(EntityTypeBuilder<Domain.Identity.GroupPermission> builder)
        {
            builder
                 .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            builder
                .HasOne(gp => gp.Group)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(gp => gp.GroupId);

            builder
                .HasOne(gp => gp.Permission)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(gp => gp.PermissionId);
        }
    }
}
