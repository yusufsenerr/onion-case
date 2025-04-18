using API.Common.Application.DTOs.Commands.Autentication;
using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Permissions.Create.UserPermissions.Create
{
    public class CreateUserPermissionCommandRequest : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }
        public List<MenuPermission> Permissions { get; set; }
     
    }
}
