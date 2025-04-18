namespace API.Common.Application.DTOs.Queries.Permission
{
    public class MenuUserPermissionDto
    {
        public Guid MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}
