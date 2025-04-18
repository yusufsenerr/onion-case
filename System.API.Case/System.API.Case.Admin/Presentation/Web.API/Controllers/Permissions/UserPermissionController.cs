using API.Common.Application.Features.Commands.Permissions.Create.UserPermissions.Create;
using API.Common.Application.Features.Queries.Permissions.UserPermissions;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUserPermission(CreateUserPermissionCommandRequest createUserPermission)
        {
            BaseResponse response = await this.mediator.Send(createUserPermission);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetUserPermission(GetUserPermissionQueryRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }

    }
}
