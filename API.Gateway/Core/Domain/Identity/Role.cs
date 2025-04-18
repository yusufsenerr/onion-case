using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<User> Users { get; set; }
    }
}
