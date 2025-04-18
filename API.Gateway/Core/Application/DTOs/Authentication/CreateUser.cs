namespace Application.DTOs.Authentication
{
    public class CreateUser
    {
        public string Username => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; } 
        public List<Guid> GroupIds { get; set; }
    }
}
