using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Permissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Permissions.Create
{
    public class CreatePermissionsCommandHandler<TDbContext>(IWriteRepository<TDbContext, Permission> writeRepository) : IRequestHandler<CreatePermissionsCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, Permission> writeRepository = writeRepository;
        public async Task<BaseResponse> Handle(CreatePermissionsCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = new Permission { 
                Id = request.Id, 
                Create = request.Create,
                Delete = request.Delete,
                Read = request.Read,
                Update = request.Update,
                Excel = request.Excel,
                Pdf = request.Pdf,
                CreatedDate = DateTime.Now
                };
            return await writeRepository.CreateOrUpdateAsync(entity);
        }
    }
}