using API.Common.Domain.Permissions;
using MediatR;

namespace API.Common.Application.Features.Queries.Permissions
{
    public class GetAllPermissionsQueryRequest : IRequest<List<Permission>>
    {
    }
}
