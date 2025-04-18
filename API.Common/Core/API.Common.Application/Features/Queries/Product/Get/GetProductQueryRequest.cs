using API.Common.Application.DTOs.Queries.Product;
using MediatR;

namespace API.Common.Application.Features.Queries.Product.Get
{
    public class GetProductQueryRequest:IRequest<List<ProductDto>>
    {
    }
}
