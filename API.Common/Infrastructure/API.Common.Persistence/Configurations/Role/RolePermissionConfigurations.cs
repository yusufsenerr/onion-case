using API.Common.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Common.Persistence.Configurations.Role;

public class RolePermissionConfigurations: IEntityTypeConfiguration<RoleMenuPermissions>
{
    public void Configure(EntityTypeBuilder<RoleMenuPermissions> builder)
    {
        builder.HasKey(dm => new { dm.AppRoleId});

        builder.HasOne(dm => dm.AppRole)
            .WithMany(d => d.RoleMenuPermissions)
            .HasForeignKey(dm => dm.AppRoleId);

    }

}