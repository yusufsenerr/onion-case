using API.Common.Domain.Commons;
using API.Common.Domain.Roles;

namespace API.Common.Domain.Menu
{
    public class MenuItem : BaseEntity
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string RouterLink { get; set; }

        // Parent-Child ilişkisi
        public Guid? ParentMenuItemId { get; set; }  // Parent menu ID'si
        public MenuItem ParentMenuItem { get; set; } // Navigasyon özelliği

        // Alt menüler (children)
        public ICollection<MenuItem> Items { get; set; }
        public ICollection<MenuButtonPermission> MenuButtonPermissions { get; set; }
        public ICollection<RoleMenuPermissions> RoleMenuPermissions { get; set; }


    }
}
