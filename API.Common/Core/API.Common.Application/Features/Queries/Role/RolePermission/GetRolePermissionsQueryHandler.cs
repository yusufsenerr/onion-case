using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Domain.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Role.RolePermission
{
    public class GetRolePermissionsQueryHandler<TDbContext>(IReadRepository<TDbContext, RoleMenuPermissions> readRepository) : IRequestHandler<GetRolePermissionsQueryRequest, List<RoleMenuPermissions>> where TDbContext : DbContext
    {
        readonly private IReadRepository<TDbContext, RoleMenuPermissions> readRepository = readRepository;
        public async Task<List<RoleMenuPermissions>> Handle(GetRolePermissionsQueryRequest request, CancellationToken cancellationToken)
        {
            return await this.readRepository
                .GetWhere(x => request.RoleId == x.AppRoleId)
                .Include(x => x.MenuItem)
                .ToListAsync(cancellationToken);
        }
    }
}
