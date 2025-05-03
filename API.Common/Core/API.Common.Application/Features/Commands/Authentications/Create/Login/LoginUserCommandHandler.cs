using API.Common.Application.JwtTokens;
using API.Common.Application.Services.LogService;
using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Commands.Authentications.Create.Login
{
    public class LoginUserCommandHandler<TContext>(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        LoggingService loggingService,
        GenerateJwtToken generateJwtToken
    ) : IRequestHandler<LoginUserCommandRequest, BaseResponse> where TContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;
        readonly SignInManager<AppUser> signInManager = signInManager;
        readonly GenerateJwtToken generateJwtToken = generateJwtToken;
        readonly LoggingService loggingService = loggingService;

        public async Task<BaseResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.Users.Include(x=>x.Role).FirstOrDefaultAsync(x=>x.IdentityNumber == request.TcIdentityNumber);

            if (user == null)
            {
                this.loggingService.LogAction("User", $"Geçersiz T.C Kimlik numarası ile oturum açma girişimi : {request.TcIdentityNumber}",user);
                return new BaseResponse
                {
                    Message = "Geçersiz T.C Kimlik numarası veya şifre.",
                    Succeeded = false
                };
            }

            this.loggingService.LogAction("User",
                $"Kullanıcı giriş denemesi: {request.TcIdentityNumber} - {user.Id.ToString()} ",user);

            if (await this.userManager.IsLockedOutAsync(user))
            {
                this.loggingService.LogAction("User",
                    $"Hesap kilitli! Kullanıcı giriş yapamadı: {request.TcIdentityNumber} ",user);
                return new BaseResponse
                {
                    Message = "Hesap kilitli. Lütfen daha sonra tekrar deneyiniz.",
                    Succeeded = false
                };
            }

            var result = await this.signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                this.loggingService.LogAction("User",
                    $"Geçersiz şifre denemesi: {request.TcIdentityNumber} ",user);

                await this.userManager.AccessFailedAsync(user);

                if (await this.userManager.IsLockedOutAsync(user))
                {
                    this.loggingService.LogAction("User",
                        $"Çok sayıda başarısız girişten dolayı hesap kilitlendi: {request.TcIdentityNumber} ",user);
                    return new BaseResponse
                    {
                        Message = "Çok sayıda başarısız giriş denemesi nedeniyle hesap kilitlendi.",
                        Succeeded = false
                    };
                }

                return new BaseResponse
                {
                    Message = "Geçersiz T.C Kimlik numarası veya şifre.",
                    Succeeded = false
                };
            }

            this.loggingService.LogAction("User", $"Başarılı giriş: {request.TcIdentityNumber} ",user);
            await this.userManager.ResetAccessFailedCountAsync(user);
            var userToken = this.generateJwtToken.JwtTokenGenerate(user);

            this.loggingService.LogAction("User",
                $"JWT token ve Refresh Token olusturuldu: {request.TcIdentityNumber} ",user);

            return new BaseResponse
            {
                Message = "Giriş başarılı.",
                Succeeded = true,
                Data = new
                {
                    AccessToken = userToken.Result,
                }
            };
        }
    }
}