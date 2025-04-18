using Microsoft.AspNetCore.Authorization;

namespace API.Common.Application.Abstractions.MenuRequirement
{
    public class MenuAccessHandler : AuthorizationHandler<MenuAccessRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MenuAccessRequirement requirement)
        {
            // Kullanıcının Menüsünü Token'dan Alıyoruz.
            var userMenus = context.User.FindAll("Menus").Select(c => c.Value).ToList();

            // İstenen Menü Yetkisini Kontrol Ediyoruz
            var requestedMenu = context.Resource as string; // Menü adı Controller'dan almak için 
            if (userMenus.Contains(requestedMenu))
            {
                context.Succeed(requirement); // Yetki Vermek için
            }

            return Task.CompletedTask;
        }
    }
}
