using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Permission
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Domain.Identity.Permission>
    {
        public void Configure(EntityTypeBuilder<Domain.Identity.Permission> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.HasData(
                    new Domain.Identity.Permission { Id = Guid.NewGuid(), Name = "Create", CreatedDate = DateTime.Now },
                    new Domain.Identity.Permission { Id = Guid.NewGuid(), Name = "Read", CreatedDate = DateTime.Now },
                    new Domain.Identity.Permission { Id = Guid.NewGuid(), Name = "Update", CreatedDate = DateTime.Now },
                    new Domain.Identity.Permission { Id = Guid.NewGuid(), Name = "Delete", CreatedDate = DateTime.Now }
                );
        }
    }
}
