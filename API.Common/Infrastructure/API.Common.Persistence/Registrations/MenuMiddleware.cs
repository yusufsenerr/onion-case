using API.Common.Application.Abstractions.MenuRequirement;
using Microsoft.AspNetCore.Http;

namespace API.Common.Persistence.Registrations
{
    public class MenuMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;
            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var permissionAttributes = endpoint.Metadata.GetOrderedMetadata<PermissionAttribute>();

                foreach (var permissionAttribute in permissionAttributes)
                {
                    var requiredMenu = permissionAttribute.RequiredMenu;
                    var menus = user.Claims.Where(c => c.Type == "Menus").Select(c => c.Value).ToList();

                    if (!menus.Contains(requiredMenu))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await context.Response.WriteAsync("Bu sayfaya erişim yetkiniz yoktur.");
                        return;
                    }
                }
            }

            await this.next(context);
        }
    }
}