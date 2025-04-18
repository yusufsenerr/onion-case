using API.Common.Persistence.Configurations.Role;
using API.Common.Persistence.Configurations.User;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Persistence.Configurations
{
    public static class ModelConfigurations
    {
        public static void ApplyAllConfigurations(ModelBuilder modelBuilder)
        {
            #region User - Role 
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new RolePermissionConfigurations());
            #endregion
        }
    }
}
