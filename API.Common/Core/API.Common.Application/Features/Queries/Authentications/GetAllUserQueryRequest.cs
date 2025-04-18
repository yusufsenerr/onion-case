using API.Common.Application.DTOs.Queries.Autentication;
using API.Common.Domain.Users;
using MediatR;

namespace API.Common.Application.Features.Queries.Authentications
{
    public class GetAllUserQueryRequest : IRequest<List<UserDto>>
    {
    }
}

