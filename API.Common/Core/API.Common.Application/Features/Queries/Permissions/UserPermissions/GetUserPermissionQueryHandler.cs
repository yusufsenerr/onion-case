using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Domain.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Permissions.UserPermissions
{
    public class GetUserPermissionQueryHandler<TDbContext>(IReadRepository<TDbContext, MenuButtonPermission> readRepository) : IRequestHandler<GetUserPermissionQueryRequest, List<MenuButtonPermission>> where TDbContext : DbContext
    {
        readonly private IReadRepository<TDbContext, MenuButtonPermission> readRepository = readRepository;
        public async Task<List<MenuButtonPermission>> Handle(GetUserPermissionQueryRequest request, CancellationToken cancellationToken)
        {
            return await this.readRepository
                .GetWhere(x => request.UserId == x.AppUserId)
                .Include(x => x.MenuItem)
                .ToListAsync(cancellationToken);

        }

    }
}
