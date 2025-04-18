using API.Common.Domain.Commons;
using MediatR;

namespace API.Common.Application.Features.Commands.Authentications.Create.Login
{
    public class LoginUserCommandRequest : IRequest<BaseResponse>
    {
        public string TcIdentityNumber { get; set; }
        public string Password { get; set; }
    }
}
