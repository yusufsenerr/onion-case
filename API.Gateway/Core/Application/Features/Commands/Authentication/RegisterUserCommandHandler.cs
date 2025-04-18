using Application.Abstractions.User;
using Application.DTOs.Authentication;
using Domain;
using MediatR;
using WatchDog;

namespace Application.Features.Commands.Authentication
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, BaseResponse>
    {
        readonly IUserService userService;
        public RegisterUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<BaseResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                WatchLogger.Log($"Kullanıcı oluşturma işlemi başlatıldı: {request.Username}");

                BaseResponse res = await this.userService.CreateUser(new CreateUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = request.Password,
                    RoleName = request.RoleName,
                    GroupIds = request.GroupIds 
                });

                if (res.Succeeded)
                {
                    WatchLogger.Log($"Kullanıcı başarılı şekilde oluşturuldu: {request.Username}");
                }
                else
                {
                    WatchLogger.LogError($"Kullanıcı oluşturulurken bir hata meydana geldi: {request.Username}, Hata Mesajı: {res.Message}");
                }

                return new BaseResponse
                {
                    Message = res.Message,
                    Succeeded = res.Succeeded,
                };
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Kullanıcı oluşturma işlemi sırasında bir istisna oluştu: {request.Username}, Hata: {ex.Message}");
                return new BaseResponse
                {
                    Message = "Bir hata meydana geldi: " + ex.Message,
                    Succeeded = false,
                };
            }
        }
    }
}
