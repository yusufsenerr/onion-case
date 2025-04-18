using API.Common.Application.DTOs.Queries.Autentication;
using API.Common.Application.Services.LogService;
using API.Common.Domain.Users;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Common.Application.Features.Queries.Authentications
{
    public class GetAllUserQueryHandler<TDbContext>(
        UserManager<AppUser> userManager,
        LoggingService loggingService,
        IMapper mapper) : IRequestHandler<GetAllUserQueryRequest, List<UserDto>> where TDbContext : DbContext
    {
        readonly private LoggingService loggingService = loggingService;
        readonly private UserManager<AppUser> userManager = userManager;
        readonly private IMapper mapper = mapper;

        public async Task<List<UserDto>> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await this.userManager.Users.ToListAsync(cancellationToken);
            this.loggingService.LogAction("User","Tüm Kullanıcıları Görüntüledi!", users);
            return this.mapper.Map<List<UserDto>>(users);
        }
    }
}