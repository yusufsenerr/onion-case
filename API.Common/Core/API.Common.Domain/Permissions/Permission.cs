using API.Common.Domain.Commons;

namespace API.Common.Domain.Permissions
{
    public class Permission : BaseEntity
    {
        public bool Create { get; set; }
        public bool Read { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Pdf { get; set; }
        public bool Excel { get; set; }
    }
}