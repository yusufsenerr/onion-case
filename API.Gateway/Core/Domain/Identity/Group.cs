
namespace Domain.Identity
{
    public class Group : BaseEntity
    {
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<GroupPermission> GroupPermissions { get; set; }
    }
}
