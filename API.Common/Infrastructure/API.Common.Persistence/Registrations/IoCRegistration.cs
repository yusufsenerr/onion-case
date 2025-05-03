using API.Common.Application.Abstractions.UserService;
using API.Common.Application.Interfaces.IReadRepositories;
using API.Common.Application.Interfaces.IWriteRepositories;
using API.Common.Application.Services.LogService;
using API.Common.Persistence.Repositories.ReadRepositories;
using API.Common.Persistence.Repositories.WriteRepositories;
using API.Common.Persistence.Services.UserServices;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Common.Persistence.Registrations
{
    public static class IoCRegistration
    {
        public static void IoCServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            services.AddScoped(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));
            services.AddScoped(typeof(IUserService<>), typeof(UserService<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddHttpContextAccessor();
            services.AddScoped<LoggingService>();
            services.AddValidationServices();
        }

    }
}