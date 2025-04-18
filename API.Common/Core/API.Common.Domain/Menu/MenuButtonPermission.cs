using API.Common.Domain.Commons;
using API.Common.Domain.Users;

namespace API.Common.Domain.Menu
{
    public class MenuButtonPermission : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool ExportPdf { get; set; }
        public bool ExportExcel { get; set; }
    }
}
