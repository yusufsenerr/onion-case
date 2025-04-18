using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Domain.Permissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Permissions
{
    public class GetAllPermissionsQueryHandler<TDbContext>(IReadRepository<TDbContext, Permission> readRepository) : IRequestHandler<GetAllPermissionsQueryRequest, List<Permission>> where TDbContext : DbContext
    {
        readonly private IReadRepository<TDbContext, Permission> readRepository = readRepository;
        public async Task<List<Permission>> Handle(GetAllPermissionsQueryRequest request, CancellationToken cancellationToken)
        {
            return await this.readRepository.GetAll().ToListAsync();
        }
    }
}
