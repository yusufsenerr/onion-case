using API.Common.Domain.Commons;
using API.Common.Domain.Menu;

namespace API.Common.Domain.Roles;

public class RoleMenuPermissions : BaseEntity
{
    public Guid AppRoleId { get; set; }
    public AppRole AppRole { get; set; }
    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    public bool Create { get; set; }
    public bool Read { get; set; }
    public bool Update { get; set; }
    public bool Delete { get; set; }
    public bool ExportPdf { get; set; }
    public bool ExportExcel { get; set; }
}