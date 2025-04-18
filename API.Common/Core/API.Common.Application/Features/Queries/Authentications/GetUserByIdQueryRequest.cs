using API.Common.Application.DTOs.Queries.Autentication;
using MediatR;

namespace API.Common.Application.Features.Queries.Authentications
{
    public class GetUserByIdQueryRequest : IRequest<UserDto>
    {
        public Guid Id{ get; set; }
      
    }
}