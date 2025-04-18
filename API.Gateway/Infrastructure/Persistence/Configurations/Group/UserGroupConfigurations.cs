using Microsoft.EntityFrameworkCore;

namespace Persistence.Configurations.Group
{
    public class UserGroupConfigurations : IEntityTypeConfiguration<Domain.Identity.UserGroup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Identity.UserGroup> builder)
        {
            builder
               .HasKey(ug => new { ug.UserId, ug.GroupId });

            builder
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            builder
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

        }
    }
}
