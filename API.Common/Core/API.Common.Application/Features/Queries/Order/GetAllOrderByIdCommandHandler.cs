using API.Common.Application.DTOs.Queries.Order;
using API.Common.Application.Interfaces.IReadRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Order
{
    public class GetAllOrderByIdCommandHandler<TDbContext>(
    IReadRepository<TDbContext, API.Common.Domain.Orders.Order> orderRepository
) : IRequestHandler<GetAllOrderByIdCommandRequest, List<OrderDto>> where TDbContext : DbContext
    {
        public async Task<List<OrderDto>> Handle(GetAllOrderByIdCommandRequest request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository
                .GetWhere(o => o.AppUserId == request.AppUserId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync(cancellationToken);

            var result = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity
                }).ToList()
            }).ToList();

            return result;
        }
    }
}
