namespace Domain.Identity
{
    public class GroupPermission : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
