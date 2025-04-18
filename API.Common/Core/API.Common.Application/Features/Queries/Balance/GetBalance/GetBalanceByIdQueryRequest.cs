using API.Common.Application.DTOs.Queries.Balance;
using API.Common.Domain.Balance;
using MediatR;

namespace API.Common.Application.Features.Queries.Balance.GetBalance
{
    public class GetBalanceByIdQueryRequest:IRequest<UserBalanceDto>
    {
        public Guid Id { get; set; }
    }
}
