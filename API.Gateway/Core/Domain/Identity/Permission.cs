namespace Domain.Identity
{
    public class Permission : BaseEntity
    {
        public ICollection<GroupPermission> GroupPermissions { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
