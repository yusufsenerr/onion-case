using API.Common.Application.Services.LogService;
using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Authentications.Update
{
    public class UpdateUserCommandHandler<TDbContext>(UserManager<AppUser> userManager, LoggingService loggingService)
        : IRequestHandler<UpdateUserCommandRequest, BaseResponse>
        where TDbContext : DbContext
    {
        readonly private UserManager<AppUser> userManager = userManager;
        readonly private LoggingService loggingService = loggingService;

        public async Task<BaseResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                return new BaseResponse
                {
                    Succeeded = false,
                    Message = "Kullanıcı bulunamadı."
                };
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.Phone;
            user.IdentityType = request.IdentityType;
            user.IdentityNumber = request.IdentityNumber;

            if (user.Email != request.Email)
            {
                var setEmailResult = await this.userManager.SetEmailAsync(user, request.Email);
                if (!setEmailResult.Succeeded)
                {
                    return new BaseResponse
                    {
                        Succeeded = false,
                        Message = "E-posta adresi güncellenemedi."
                    };
                }
            }

            if (user.UserName != request.Username)
            {
                var setUserNameResult = await this.userManager.SetUserNameAsync(user, request.Username);
                if (!setUserNameResult.Succeeded)
                {
                    return new BaseResponse
                    {
                        Succeeded = false,
                        Message = "Kullanıcı adı güncellenemedi."
                    };
                }
            }

            var updateResult = await this.userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new BaseResponse
                {
                    Succeeded = false,
                    Message = "Kullanıcı güncellenirken hata oluştu."
                };
            }

            this.loggingService.LogAction("User", $"Kullanıcı güncellendi: {request.Username} ({request.Id})", user);

            return new BaseResponse
            {
                Succeeded = true,
                Message = "Kullanıcı başarıyla güncellendi."
            };
        }
    }
}