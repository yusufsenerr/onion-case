using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Role
{
    public class RoleConfigurations : IEntityTypeConfiguration<Domain.Identity.Role>
    {
        public void Configure(EntityTypeBuilder<Domain.Identity.Role> builder)
        {

        }
    }
}
