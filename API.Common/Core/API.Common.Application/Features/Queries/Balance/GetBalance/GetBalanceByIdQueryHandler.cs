using API.Common.Application.DTOs.Queries.Balance;
using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Services.LogService;
using API.Common.Domain.Balance;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Balance.GetBalance
{
    public class GetBalanceByIdQueryHandler<TDbContext>(
        IReadRepository<TDbContext,UserBalance> readRepository,
       IMapper mapper) : IRequestHandler<GetBalanceByIdQueryRequest, UserBalanceDto> where TDbContext : DbContext
    {
        readonly private IMapper mapper = mapper;
        private readonly IReadRepository<TDbContext,UserBalance> readRepository = readRepository;
        public async Task<UserBalanceDto> Handle(GetBalanceByIdQueryRequest request, CancellationToken cancellationToken)
        {
           var data =  this.readRepository.GetWhere(x => x.AppUserId == request.Id).FirstOrDefault().Amount;
            return this.mapper.Map<UserBalanceDto>(data);
        }
    }
}
