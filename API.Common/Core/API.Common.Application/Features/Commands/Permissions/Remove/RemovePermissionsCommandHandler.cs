using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Permissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Permissions.Remove
{
    public class RemovePermissionsCommandHandler<TDbContext>(IWriteRepository<TDbContext, Permission> writeRepository) : IRequestHandler<RemovePermissionsCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, Permission> writeRepository = writeRepository;

        public async Task<BaseResponse> Handle(RemovePermissionsCommandRequest request, CancellationToken cancellationToken)
        {
            return await writeRepository.RemoveAsync(request.Id.ToString());
        }
    }
}
