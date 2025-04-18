using API.Common.Application.DTOs.Queries.Autentication;
using API.Common.Domain.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Authentications
{
    public class GetUserByIdQueryHandler<TDbContext>(UserManager<AppUser> userManager,IMapper mapper ) : IRequestHandler<GetUserByIdQueryRequest, UserDto> where TDbContext : DbContext
    {
        readonly UserManager<AppUser> userManager = userManager;
        readonly IMapper mapper = mapper;

        public async Task<UserDto> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByIdAsync(request.Id.ToString());
            return this.mapper.Map<UserDto>(user);
        }
    }
}