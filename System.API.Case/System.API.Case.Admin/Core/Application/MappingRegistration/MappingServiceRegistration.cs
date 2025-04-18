using Application.MappingRegistration.Mapper.AppUser;
using Application.MappingRegistration.Mapper.Permission;
using Application.MappingRegistration.Mapper.UserBalance;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.MappingRegistration
{
    public static class MappingServiceRegistration
    {
        public static void MappingAddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAutoMapper(typeof(AppUserProfile).Assembly);
            services.AddAutoMapper(typeof(UserBalanceProfile).Assembly);
            services.AddAutoMapper(typeof(PermissionProfile).Assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly());

        }
    }
}
