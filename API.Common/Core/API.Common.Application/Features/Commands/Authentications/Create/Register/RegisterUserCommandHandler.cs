using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Domain.Commons;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.Authentications.Create.Register
{
    public class RegisterUserCommandHandler<TDbContext>(
        UserManager<AppUser> userManager,
        IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> balanceWriteRepository

        ) : IRequestHandler<RegisterUserCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;

        readonly IWriteRepository<TDbContext, API.Common.Domain.Balance.UserBalance> balanceWriteRepository = balanceWriteRepository;

        public async Task<BaseResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await this.userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı zaten mevcut." };
                }
                var user = new AppUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityType = request.IdentityType,
                    IdentityNumber = request.IdentityNumber,
                    PhoneNumber = request.Phone,
                    RoleId = request.RoleId,
                };


                var createUserResult = await userManager.CreateAsync(user, request.Password);

                if (!createUserResult.Succeeded)
                {
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı oluşturulamadı." };
                }
                else
                {
                    var balance = new API.Common.Domain.Balance.UserBalance
                    {
                        Amount = 0,
                        Currency = "TRY",
                        AppUserId = user.Id,
                    };
                    var createBalance = await this.balanceWriteRepository.AddAsync(balance);
                }





                return new BaseResponse { Succeeded = true, Message = "Kullanıcı başarıyla kaydedildi!" };
            }
            catch (Exception ex)
            {
                WatchLogger.LogError(ex.ToString(), "Kullanıcı kaydı sırasında hata oluştu.");
                return new BaseResponse { Succeeded = false, Message = "Kullanıcı kaydı sırasında bir hata meydana geldi!" };
            }
        }
    }
}
