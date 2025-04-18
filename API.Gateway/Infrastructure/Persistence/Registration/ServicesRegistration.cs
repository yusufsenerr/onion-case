using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using WatchDog;
using WatchDog.src.Enums;

namespace Persistence.Registration
{
    public static class ServicesRegistration
    {   
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<APIGatewayDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Local"));
            });

            services.AddWatchDogServices(opt =>
            {
                opt.IsAutoClear = true;
                opt.SetExternalDbConnString = configuration.GetConnectionString("Log");
                opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
            });
        }

    }
}
