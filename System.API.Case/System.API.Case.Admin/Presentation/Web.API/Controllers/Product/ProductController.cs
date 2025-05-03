using API.Common.Application.Features.Commands.Product.Create;
using API.Common.Application.Features.Commands.Product.Remove;
using API.Common.Application.Features.Queries.Product.Get;
using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        readonly IMediator mediator = mediator;
        [Authorize(Roles = "SystemAdministrators")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest createProductCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(createProductCommandRequest);
            return Ok(response);
        }
        [Authorize(Roles = "SystemAdministrators")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllProduct()
        {
            var response = await this.mediator.Send(new GetProductQueryRequest());
            return Ok(response);
        }
        [Authorize(Roles = "SystemAdministrators")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RemoveProduct(RemoveProductCommandRequest removeProductCommandRequest)
        {
            BaseResponse response = await this.mediator.Send(removeProductCommandRequest);
            return Ok(response);
        }
    }
}
