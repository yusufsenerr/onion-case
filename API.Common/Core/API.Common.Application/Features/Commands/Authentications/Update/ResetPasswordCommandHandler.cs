using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Authentications.Update
{
    public class ResetPasswordCommandHandler<TDbContext>(
        UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        IHttpContextAccessor _httpContextAccessor,
        IMapper mapper) : IRequestHandler<ResetPasswordCommandRequest, BaseResponse> where TDbContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;
        readonly private IMapper mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task<BaseResponse> Handle(ResetPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
                return new BaseResponse { Succeeded = false, Message = "Kullanıcı oturumu bulunamadı." };

            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            return new BaseResponse
            {
                Message = "Kullanıcı bulunamadı.",
                Succeeded = false
            };

            var checkPassword = await userManager.CheckPasswordAsync(user, request.CurrentPassword);
            if (!checkPassword)
            return new BaseResponse
            {
                Message = "Mevcut şifre hatalı!",
                Succeeded = false
            };

            var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
            return new BaseResponse
            {
                Message = "Şifre değiştirme başarısız!",
                Succeeded = false
            };
            return new BaseResponse
            {
                Message = "Şifre başarıyla değiştirildi.",
                Succeeded = true
            };

        }
    }
}
