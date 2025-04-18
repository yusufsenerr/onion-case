using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Authentications.Remove
{
    public class RemoveUserCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
