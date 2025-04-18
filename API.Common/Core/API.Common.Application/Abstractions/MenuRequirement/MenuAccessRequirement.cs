using Microsoft.AspNetCore.Authorization;

namespace API.Common.Application.Abstractions.MenuRequirement
{
    public class MenuAccessRequirement : IAuthorizationRequirement
    {
        public MenuAccessRequirement() { }
    }
}
