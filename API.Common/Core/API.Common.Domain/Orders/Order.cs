using API.Common.Domain.Commons;
using API.Common.Domain.Users;

namespace API.Common.Domain.Orders
{
    public  class Order :BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
