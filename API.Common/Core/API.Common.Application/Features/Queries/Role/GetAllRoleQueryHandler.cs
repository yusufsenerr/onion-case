using API.Common.Domain.Roles;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace API.Common.Application.Features.Queries.Role
{
    public class GetAllRoleQueryHandler<TDbContext>(RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRoleQueryRequest, List<AppRole>> where TDbContext : DbContext
    {
        readonly private RoleManager<AppRole> roleManager = roleManager;

        public async Task<List<AppRole>> Handle(GetAllRoleQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = this.roleManager.Roles.AsQueryable();
                var roles = await query.ToListAsync();

                return roles;
            }
            catch (Exception ex)
            {
                WatchLogger.LogError($"Roller alınırken bir hata oluştu: {ex.Message}");
                throw;
            }

        }
    }
}
