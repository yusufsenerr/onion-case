using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace Persistence.Registration
{
    public static class OcelotRegistration
    {
        public static void AddOcelotServices(this IServiceCollection services, IConfiguration configuration)
        {
            var configurationBuilder = new ConfigurationBuilder()
                                                                .SetBasePath(Directory.GetCurrentDirectory()) 
                                                                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true) 
                                                                .AddEnvironmentVariables();
            IConfiguration ocelotConfiguration = configurationBuilder.Build();
            services.AddOcelot(ocelotConfiguration);
        }
    }
}
