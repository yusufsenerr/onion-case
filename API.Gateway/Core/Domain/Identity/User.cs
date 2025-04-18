using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public Guid? RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }

    }
}
