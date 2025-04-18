using API.Common.Application.Features.Commands.Role.Create;
using API.Common.Application.Features.Queries.Role.RolePermission;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Permissions
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRolePermissions(CreateRolePermissionsCommandRequest createRolePermissionsCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createRolePermissionsCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolePermissions(GetRolePermissionsQueryRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
