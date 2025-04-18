using API.Common.Domain.Roles;
using MediatR;

namespace API.Common.Application.Features.Queries.Role.RolePermission
{
    public class GetRolePermissionsQueryRequest : IRequest<List<RoleMenuPermissions>>
    {
        public Guid RoleId { get; set; }
    }
}
