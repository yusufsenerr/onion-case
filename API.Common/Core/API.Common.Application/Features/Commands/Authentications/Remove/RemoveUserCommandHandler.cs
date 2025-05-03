using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.Authentications.Remove
{
    public class RemoveUserCommandHandler<TDbContext>(
        UserManager<AppUser> userManager
    ) : IRequestHandler<RemoveUserCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;

        public async Task<BaseResponse> Handle(RemoveUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                {
                    return new BaseResponse
                    {
                        Message = "Kullanıcı bulunamadı.",
                        Succeeded = false
                    };
                }
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    var errorMessage = $"Kullanıcı silinemedi: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                    WatchLogger.LogError($"Kullanıcı silme hatası: {user.Id}, Hata: {errorMessage}");
                    return new BaseResponse
                    {
                        Message = "Kullanıcı silinemedi: " + string.Join(", ", result.Errors.Select(e => e.Description)),
                        Succeeded = false
                    };
                }

                return new BaseResponse
                {
                    Message = "Kullanıcı ve ilişkili izinler başarıyla silindi.",
                    Succeeded = true
                };
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Kullanıcı silme işlemi sırasında bir istisna oluştu: Kullanıcı ID: {request.Id}, Hata: {ex.Message}");
                return new BaseResponse
                {
                    Message = "Bir hata meydana geldi: " + ex.Message,
                    Succeeded = false
                };
            }
        }
    }
}
