using API.Common.Domain.Commons;
using API.Common.Domain.Users;

namespace API.Common.Domain.Balance
{
    public class UserBalance:BaseEntity
    {

        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
