namespace API.Common.Application.DTOs.Commands.Autentication
{
    public class UpdateUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public List<Guid> GroupIds { get; set; }
    }
}
