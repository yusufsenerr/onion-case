using API.Common.Application.Features.Commands.Product.Create;
using API.Common.Application.Features.Commands.Product.Remove;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createProductCommandRequest);
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveProduct(RemoveProductCommandRequest removeProductCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(removeProductCommandRequest);
            return Ok(response);
        }
    }
}
