using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace dotnet_bitcoin_api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // services.AddTransient<ITopicAreaService, TopicAreaService>();
            // services.AddScoped<ISecurityService, SecurityService>();
            // services.AddSingleton<ICachingService, CachingService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "dotnet-bitcoin-api", Version = "1.0" });
            });

            return services;
        }
    }
}
