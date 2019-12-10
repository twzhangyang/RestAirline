using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestAirline.Shared
{
    public class ConfigurationRootCreatorForTesting
    {
        public static IConfiguration Create(IServiceCollection services)
        {
            services.AddOptions();
            
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("unittest.json",
                optional: false);
            
            var ConfigurationRoot = configurationBuilder.Build();
            services.AddSingleton((IConfiguration)ConfigurationRoot);

            return ConfigurationRoot;
        }
    }
}