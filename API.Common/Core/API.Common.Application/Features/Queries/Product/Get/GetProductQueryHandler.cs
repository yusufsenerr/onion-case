using API.Common.Application.DTOs.Queries.Product;
using API.Common.Application.Interfaces.IReadRepositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Product.Get
{
    public class GetProductQueryHandler<TDbContext>(
        IReadRepository<TDbContext, API.Common.Domain.Product.Product> readRepository,
       IMapper mapper) : IRequestHandler<GetProductQueryRequest, List<ProductDto>> where TDbContext : DbContext
    {
        readonly private IMapper mapper = mapper;
        private readonly IReadRepository<TDbContext, API.Common.Domain.Product.Product> readRepository = readRepository;
        public async Task<List<ProductDto>> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await  this.readRepository.GetAll(x=>x.ProductImages).Where(x=>x.IsDeleted != true).ToListAsync();
            return this.mapper.Map<List<ProductDto>>(data);
        }
    }
}
