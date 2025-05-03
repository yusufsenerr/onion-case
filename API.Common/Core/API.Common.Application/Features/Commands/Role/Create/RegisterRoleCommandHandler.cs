using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.Role.Create
{
    public class RegisterRoleCommandHandler<TDbContext> : IRequestHandler<RegisterRoleCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly private RoleManager<AppRole> roleManager;
        public RegisterRoleCommandHandler(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<BaseResponse> Handle(RegisterRoleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var existingRole = await roleManager.FindByIdAsync(request.Id.ToString());

                if (existingRole != null) 
                {
                    existingRole.Name = request.Name;

                    var updateResult = await roleManager.UpdateAsync(existingRole);
                    if (updateResult.Succeeded)
                    {
                        return new BaseResponse { Message = "Rol başarıyla güncellendi.", Succeeded = true };
                    }
                    else
                    {
                        return new BaseResponse { Message = "Rol güncellenirken bir hata oluştu.", Succeeded = false };
                    }
                }
                else
                {
                    var roleExistsByName = await roleManager.RoleExistsAsync(request.Name);
                    if (roleExistsByName)
                    {
                        return new BaseResponse { Message = "Bu isimde bir rol zaten mevcut.", Succeeded = false };
                    }

                    var createResult = await roleManager.CreateAsync(new AppRole { Id = Guid.Parse(request.Id), Name = request.Name });
                    if (createResult.Succeeded)
                    {
                        return new BaseResponse { Message = "Rol başarıyla oluşturuldu.", Succeeded = true };
                    }
                    else
                    {
                        return new BaseResponse { Message = "Rol oluşturulurken bir hata meydana geldi.", Succeeded = false };
                    }
                }
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Rol işleme sırasında bir istisna oluştu: {request.Id} - {request.Name}, Hata: {ex.Message}");
                return new BaseResponse
                {
                    Message = "Bir hata meydana geldi: " + ex.Message,
                    Succeeded = false,
                };
            }
        }
    }
}