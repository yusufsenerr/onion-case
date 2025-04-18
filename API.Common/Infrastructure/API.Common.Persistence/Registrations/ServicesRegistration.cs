using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchDog;
using WatchDog.src.Enums;

namespace API.Common.Persistence.Registrations
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceServices<TContext,TLogContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName, string connectionLog)
       where TContext : DbContext
        where TLogContext : DbContext
        {
            services.AddDbContext<TContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });
            // services.AddDbContext<TContext>(opt =>
            // {
            //     opt.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            // });

            services.AddWatchDogServices(opt =>
            {
                opt.IsAutoClear = true;
                opt.SetExternalDbConnString = configuration.GetConnectionString(connectionStringName);
                opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.MaxDepth = 64;
            });
        }
    }
}
