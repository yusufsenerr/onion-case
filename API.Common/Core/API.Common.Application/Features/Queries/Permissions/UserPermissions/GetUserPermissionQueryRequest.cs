using API.Common.Domain.Menu;
using MediatR;

namespace API.Common.Application.Features.Queries.Permissions.UserPermissions
{
    public class GetUserPermissionQueryRequest:IRequest<List<MenuButtonPermission>>
    {
        public Guid UserId { get; set; }
    }
}
