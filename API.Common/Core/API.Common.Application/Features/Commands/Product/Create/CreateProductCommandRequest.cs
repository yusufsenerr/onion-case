using API.Common.Domain.Commons;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace API.Common.Application.Features.Commands.Product.Create
{
    public class CreateProductCommandRequest:IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public List<IFormFile> Images { get; set; } = new();
    }
}
