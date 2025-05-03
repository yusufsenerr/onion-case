using API.Common.Domain.Orders;
using API.Common.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace API.Common.Domain.Users
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public Guid RoleId { get; set; }
        public AppRole Role { get; set; }
        public API.Common.Domain.Balance.UserBalance UserBalance { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}