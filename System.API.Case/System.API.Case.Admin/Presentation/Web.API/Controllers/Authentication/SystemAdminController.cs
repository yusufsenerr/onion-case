using API.Common.Application.Features.Commands.SystemAdmin;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAdminController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;

        [Authorize(Roles = "SystemAdministrators")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSystemAdmin(CreateSystemAdminCommandRequest request)
        {
            BaseResponse response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
