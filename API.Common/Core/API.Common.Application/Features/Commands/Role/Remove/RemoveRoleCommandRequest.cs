using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Role.Remove
{
    public class RemoveRoleCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
