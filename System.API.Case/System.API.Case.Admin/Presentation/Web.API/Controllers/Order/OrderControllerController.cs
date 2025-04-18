using API.Common.Application.Features.Commands.Order;
using API.Common.Application.Features.Queries.Order;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Order
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderControllerController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest createProductCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createProductCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllOrderById(GetAllOrderByIdCommandRequest request)
        {
            var response = await this.mediator.Send(request);
            return Ok(response);
        }
    }
}
