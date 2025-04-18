using API.Common.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Common.Domain.Roles
{
    public class AppRole : IdentityRole<Guid>
    {
        public ICollection<AppUser> Users { get; set; }
        public ICollection<RoleMenuPermissions> RoleMenuPermissions { get; set; }
    }
}