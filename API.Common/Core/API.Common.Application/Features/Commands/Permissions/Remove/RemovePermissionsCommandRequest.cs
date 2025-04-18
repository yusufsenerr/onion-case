using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Permissions.Remove
{
    public class RemovePermissionsCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
