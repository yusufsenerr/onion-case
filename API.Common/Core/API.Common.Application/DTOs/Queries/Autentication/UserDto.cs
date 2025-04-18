using API.Common.Application.DTOs.Queries.Role;

namespace API.Common.Application.DTOs.Queries.Autentication
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public RoleDto Role { get; set; }
    }

}


