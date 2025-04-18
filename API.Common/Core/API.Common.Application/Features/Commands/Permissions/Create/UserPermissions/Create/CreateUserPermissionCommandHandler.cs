using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Menu;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Permissions.Create.UserPermissions.Create
{
    public class CreateUserPermissionCommandHandler<TDbContext>(
        IWriteRepository<TDbContext, MenuButtonPermission> writeRepository)
        : IRequestHandler<CreateUserPermissionCommandRequest, BaseResponse>
        where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, MenuButtonPermission> writeRepository = writeRepository;
        public async Task<BaseResponse> Handle(CreateUserPermissionCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.AppUserId == null || request.Permissions == null || !request.Permissions.Any())
            {
                return new BaseResponse{Succeeded = false, Message = "Kullanıcı veya Yetkiler boş olamaz."};
            }
            var permissionsToSave = request.Permissions
                .Select(permission => new MenuButtonPermission
                {
                    Id = permission.Id,
                    AppUserId = request.AppUserId,
                    MenuItemId = permission.MenuItemId,
                    Create = permission.Add,
                    Read = permission.Read,
                    Update = permission.Update,
                    Delete = permission.Delete,
                    ExportPdf = permission.Pdf,
                    ExportExcel = permission.Excel
                }).ToList();
         return await this.writeRepository.BulkCreateOrUpdateAsync(permissionsToSave);
        }
    }
}