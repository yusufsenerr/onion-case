using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using WatchDog;
using WatchDog.src.Enums;

namespace API.Common.Persistence.Registrations
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceServices<TContext>(this IServiceCollection services, IConfiguration configuration, string connectionStringName, string connectionLog)
       where TContext : DbContext
        {
            services.AddDbContext<TContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(connectionStringName));
            });

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
            services.AddScoped<UpdateOrderStatusJob>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("UpdateOrderStatusJob");

                q.AddJob<UpdateOrderStatusJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("UpdateOrderStatusJob-trigger")
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(30) // test için
                        .RepeatForever()));
            });

            services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
        }
    }
}
