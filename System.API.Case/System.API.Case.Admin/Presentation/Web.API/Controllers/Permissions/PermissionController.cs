using API.Common.Application.Features.Commands.Permissions.Create;
using API.Common.Application.Features.Commands.Permissions.Remove;
using API.Common.Application.Features.Queries.Permissions;
using API.Common.Domain.Commons;
using API.Common.Domain.Permissions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllPermissions()
        {
            List<Permission> response = await this.mediator.Send(new GetAllPermissionsQueryRequest());
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllByPermission(Guid id)
        {
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePermission(CreatePermissionsCommandRequest createPermissionsCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createPermissionsCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RemovePermission(RemovePermissionsCommandRequest removePermissionsCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(removePermissionsCommandRequest);
            return Ok(response);
        }
    }
}
