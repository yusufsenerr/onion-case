using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Product.Remove
{
    public class RemoveProductCommandRequest:IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
