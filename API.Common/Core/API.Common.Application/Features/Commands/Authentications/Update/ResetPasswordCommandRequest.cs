using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Authentications.Update
{
    public class ResetPasswordCommandRequest : IRequest<BaseResponse>
    {
        public string? UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
