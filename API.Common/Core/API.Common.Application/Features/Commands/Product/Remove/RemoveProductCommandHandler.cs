using API.Common.Application.Features.Commands.Permissions.Remove;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Product.Remove
{
    public class RemoveProductCommandHandler<TDbContext>(IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository) : IRequestHandler<RemovePermissionsCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, API.Common.Domain.Product.Product> writeRepository = writeRepository;

        public async Task<BaseResponse> Handle(RemovePermissionsCommandRequest request, CancellationToken cancellationToken)
        {
            return await writeRepository.RemoveAsync(request.Id.ToString());
        }
    }
}
