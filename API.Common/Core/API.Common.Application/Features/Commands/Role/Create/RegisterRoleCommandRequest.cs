using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Role.Create
{
    public class RegisterRoleCommandRequest : IRequest<BaseResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
