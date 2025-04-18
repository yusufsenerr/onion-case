using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Common.Application.Abstractions.MenuRequirement
{
    public class PermissionAttribute(string RequiredMenu) : Attribute, IAuthorizationFilter
    {
        public readonly string RequiredMenu = RequiredMenu;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(new { Message = "Yetkilendirme başarısız. Giriş yapın." });
                return;
            }

            var menus = user.Claims
                            .Where(c => c.Type == "Menus")
                            .Select(c => c.Value)
                            .Distinct() // Tekrarlanan değerleri kaldır
                            .ToList();

            if (!menus.Contains(this.RequiredMenu))
            {
                context.Result = new ForbidResult(); // 403 Forbidden
            }
        }
    }
}
