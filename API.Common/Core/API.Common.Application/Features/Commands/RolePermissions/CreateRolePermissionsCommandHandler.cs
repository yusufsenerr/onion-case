using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Role.Create
{
    public class CreateRolePermissionsCommandHandler<TDbContext>(
        IWriteRepository<TDbContext, API.Common.Domain.Roles.RoleMenuPermissions> roleMenuPermissionWriteRepository)
        : IRequestHandler<CreateRolePermissionsCommandRequest, BaseResponse>
        where TDbContext : DbContext
    {
        readonly private IWriteRepository<TDbContext, API.Common.Domain.Roles.RoleMenuPermissions> roleMenuPermissionWriteRepository = roleMenuPermissionWriteRepository;

        public async Task<BaseResponse> Handle(CreateRolePermissionsCommandRequest request,CancellationToken cancellationToken)
        {
            if (request.RoleId == null || request.Permissions == null || !request.Permissions.Any())
            {
                return new BaseResponse { Succeeded = false, Message = "Kullanıcı veya Yetkiler boş olamaz." };
            }
            var permissionsToSave = request.Permissions
                .Select(permission => new RoleMenuPermissions
                {
                    Id = permission.Id,
                    AppRoleId = request.RoleId,
                    MenuItemId = permission.MenuItemId,
                    Create = permission.Add,
                    Read = permission.Read,
                    Update = permission.Update,
                    Delete = permission.Delete,
                    ExportPdf = permission.Pdf,
                    ExportExcel = permission.Excel
                }).ToList();
            return await this.roleMenuPermissionWriteRepository.BulkCreateOrUpdateAsync(permissionsToSave);
        }
    }
}