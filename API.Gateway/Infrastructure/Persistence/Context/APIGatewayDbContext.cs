using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Group;
using Persistence.Configurations.Permission;
using Persistence.Configurations.Role;
using Persistence.Configurations.User;

namespace Persistence.Context
{
    public class APIGatewayDbContext : IdentityDbContext<User, Role, Guid>
    {

        public APIGatewayDbContext(DbContextOptions<APIGatewayDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfigurations());
            modelBuilder.ApplyConfiguration(new UserPermissionConfigurations());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Domain.Identity.GroupPermission> GroupPermissions { get; set; }
    }
}
