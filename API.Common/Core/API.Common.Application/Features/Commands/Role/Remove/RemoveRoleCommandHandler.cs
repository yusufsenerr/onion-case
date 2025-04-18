using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.Role.Remove
{
    public class RemoveRoleCommandHandler<TDbContext>(RoleManager<AppRole> roleManager) : IRequestHandler<RemoveRoleCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly private RoleManager<AppRole> roleManager = roleManager;

        public async Task<BaseResponse> Handle(RemoveRoleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await roleManager.FindByIdAsync(request.Id.ToString());
                if (role == null)
                {
                    return new BaseResponse { Message = "Silinmek istenen rol bulunamadı.", Succeeded = false };
                }

                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return new BaseResponse { Message = "Rol başarıyla silindi.", Succeeded = true };
                }
                else
                {
                    return new BaseResponse { Message = "Rol silinirken bir hata oluştu.", Succeeded = false };
                }
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Rol silme işlemi sırasında bir istisna oluştu: {request.Id}, Hata: {ex.Message}");
                return new BaseResponse
                {
                    Message = "Bir hata meydana geldi: " + ex.Message,
                    Succeeded = false,
                };
            }
        }
    }
}
