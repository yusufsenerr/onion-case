using API.Common.Application.DTOs.Commands.Order;
using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Order
{
    public class CreateOrderCommandRequest:IRequest<BaseResponse>
    {
        public Guid AppUserId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
