namespace API.Common.Application.DTOs.Commands.Order
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
