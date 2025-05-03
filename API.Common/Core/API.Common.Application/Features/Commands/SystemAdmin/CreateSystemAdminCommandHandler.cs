using API.Common.Domain.Commons;
using API.Common.Domain.Roles;
using API.Common.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Commands.SystemAdmin
{
    public class CreateSystemAdminCommandHandler<TDbContext>(
        RoleManager<AppRole> roleManager,
      UserManager<AppUser> userManager,
       IHttpContextAccessor httpContextAccessor
        ) : IRequestHandler<CreateSystemAdminCommandRequest, BaseResponse>
    where TDbContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;
        readonly private RoleManager<AppRole> roleManager = roleManager;
        readonly private IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public async Task<BaseResponse> Handle(CreateSystemAdminCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var control = _httpContextAccessor.HttpContext?.User;
                var isAdmin = control?.IsInRole("SystemAdministrators") ?? false;
                if (!isAdmin)
                {
                    return new BaseResponse { Succeeded = false, Message = "Bu işlemi sadece admin yapabilir." };
                }

                var existingUser = await this.userManager.FindByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı zaten mevcut." };
                }
                var role = await this.roleManager.FindByNameAsync("SystemAdministrators");
                var user = new AppUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityType = request.IdentityType,
                    IdentityNumber = request.IdentityNumber,
                    PhoneNumber = request.Phone,
                    RoleId = role.Id,
                };


                var createUserResult = await userManager.CreateAsync(user, request.Password);

                if (!createUserResult.Succeeded)
                {
                    return new BaseResponse { Succeeded = false, Message = "Kullanıcı oluşturulamadı." };
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
