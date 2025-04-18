using API.Common.Domain.Commons;
using API.Common.Domain.Roles;

namespace API.Common.Domain.SystemAdmin
{
    public class SystemAdmin:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string PlaceOfBirth { get; set; } //Doğum Yeri
        public string BloodGroup { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public AppRole Role { get; set; }
    }
}
