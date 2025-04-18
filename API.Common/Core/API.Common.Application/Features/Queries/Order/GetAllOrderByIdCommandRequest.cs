using API.Common.Application.DTOs.Queries.Order;
using MediatR;

namespace API.Common.Application.Features.Queries.Order
{
    public class GetAllOrderByIdCommandRequest : IRequest<List<OrderDto>>
    {
        public Guid AppUserId { get; set; }
    }
}
