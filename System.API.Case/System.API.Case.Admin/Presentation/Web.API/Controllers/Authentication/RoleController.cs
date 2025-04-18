using API.Common.Application.Features.Commands.Role.Create;
using API.Common.Application.Features.Commands.Role.Remove;
using API.Common.Application.Features.Queries.Role;
using API.Common.Application.Features.Queries.Role.RolePermission;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllRoles(GetAllRoleQueryRequest request)
        {
            var roles = await this.mediator.Send(request);
            return Ok(roles);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(RegisterRoleCommandRequest createRoleCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveRole(RemoveRoleCommandRequest removeRoleCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(removeRoleCommandRequest);
            return Ok(response);
        }

    }
}
