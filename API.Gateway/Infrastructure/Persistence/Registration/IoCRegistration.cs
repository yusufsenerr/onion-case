using Application.Abstractions.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services.User;
using System.Reflection;

namespace Persistence.Registration
{
    public static class IoCRegistration 
    {
        public static void IoCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped(typeof(Application.Interfaces.IReadRepository<>), typeof(Repository.ReadRepository<>));
            services.AddScoped(typeof(Application.Interfaces.IWriteRepository<>), typeof(Repository.WriteRepository<>));

            services.AddScoped<IUserService,UserService>();
        }

    }
}
