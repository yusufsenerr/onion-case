using API.Common.Application.Abstractions.UserService;
using API.Common.Application.DTOs.Commands.Autentication;
using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using API.Common.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Persistence.Services.UserServices
{
    public class UserService<TContext>(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : IUserService<TContext> where TContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;
        readonly RoleManager<AppRole> roleManager = roleManager;

        public async Task<BaseResponse> UpdateUser(UpdateUserDto model)
        {
            try
            {
                // Kullanıcıyı ID ile bul
                var user = await this.userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    WatchLogger.LogError($"Kullanıcı bulunamadı: {model.UserId}");
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı bulunamadı!" };
                }

                // Kullanıcı bilgilerini güncelle
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.FirstName; // UserName için FirstName kullanılıyor

                var result = await this.userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    WatchLogger.Log($"Kullanıcı başarıyla güncellendi: {user.UserName}");

                    // Kullanıcı rolünü güncelle
                    var currentRoles = await this.userManager.GetRolesAsync(user);
                    if (currentRoles.Count > 0)
                    {
                        await this.userManager.RemoveFromRolesAsync(user, currentRoles); // Eski rollerini sil
                    }
                    var role = await this.roleManager.FindByNameAsync(model.RoleName);
                    if (role == null)
                    {
                        role = new AppRole { Name = model.RoleName };
                        var roleResult = await this.roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            WatchLogger.LogError($"Rol oluşturulurken bir hata meydana geldi: {roleResult.Errors}");
                            return new BaseResponse { Succeeded = false, Message = "Rol oluşturulurken bir hata meydana geldi!" };
                        }
                        WatchLogger.Log($"Rol oluşturuldu: {model.RoleName}");
                    }

                    await this.userManager.AddToRoleAsync(user, model.RoleName);
                    WatchLogger.Log($"Kullanıcıya yeni rol atandı: {model.RoleName}");

                    return new BaseResponse { Succeeded = true, Message = "Kullanıcı başarıyla güncellendi!" };
                }
                else
                {
                    WatchLogger.LogError($"Kullanıcı güncellenirken bir hata meydana geldi: {result.Errors}");
                    string errorMessages = string.Join(";", result.Errors.Select(e => e.Description));
                    return new BaseResponse { Succeeded = false, Message = errorMessages };
                }
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Kullanıcı güncellenirken bir hata meydana geldi: {ex.Message}");
                return new BaseResponse { Succeeded = false, Message = $"Bir hata meydana geldi: {ex.Message}" };
            }
        }
        public async Task<BaseResponse> DeleteUser(Guid id)
        {
            try
            {
                // Kullanıcıyı ID ile bul
                var user = await this.userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    WatchLogger.LogError($"Kullanıcı bulunamadı: {id}");
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı bulunamadı!" };
                }

                // Kullanıcıyı sil
                var result = await this.userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    WatchLogger.Log($"Kullanıcı başarıyla silindi: {user.UserName}");
                    return new BaseResponse { Succeeded = true, Message = "Kullanıcı başarıyla silindi!" };
                }
                else
                {
                    WatchLogger.LogError($"Kullanıcı silinirken bir hata meydana geldi: {result.Errors}");
                    string errorMessages = string.Join(";", result.Errors.Select(e => e.Description));
                    return new BaseResponse { Succeeded = false, Message = errorMessages };
                }
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Kullanıcı silinirken bir hata meydana geldi: {ex.Message}");
                return new BaseResponse { Succeeded = false, Message = $"Bir hata meydana geldi: {ex.Message}" };
            }
        }

    }
}
