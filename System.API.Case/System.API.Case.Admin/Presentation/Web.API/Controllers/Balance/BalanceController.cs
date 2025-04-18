using API.Common.Application.Features.Commands.Role.Create;
using API.Common.Application.Features.Commands.UserBalance.AddBalance;
using API.Common.Application.Features.Queries.Balance.GetBalance;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Balance
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;

        [HttpPost("[action]")]
        public async Task<IActionResult> GetBalance(GetBalanceByIdQueryRequest request)
        {
            var balance = await this.mediator.Send(request);
            return Ok(balance);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddBalance(AddUserBalanceCommandRequest addUserBalanceCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(addUserBalanceCommandRequest);
            return Ok(response);
        }
    }
}
