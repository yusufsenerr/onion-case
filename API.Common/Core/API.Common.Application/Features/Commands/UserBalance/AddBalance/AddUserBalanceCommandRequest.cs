using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.UserBalance.AddBalance
{
    public class AddUserBalanceCommandRequest:IRequest<BaseResponse>
    {
        public Guid AppUserId { get; set; }
        public decimal Balance { get; set; }
    }
}
