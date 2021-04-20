using System.Linq;
using ABB.NTier.Database;
using ABB.NTier.WebApi.Constants;
using ABB.NTier.WebApi.Services;
using ABB.NTier.WebApi.Utils;
using Boxed.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ABB.NTier.WebApi
{
    /// <summary>
    ///     The main start-up class for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">
        ///     The application configuration, where key value pair settings are stored. See
        ///     http://docs.asp.net/en/latest/fundamentals/configuration.html
        /// </param>
        /// <param name="webHostEnvironment">
        ///     The environment the application is running under. This can be Development,
        ///     Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html
        /// </param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        ///     called by the ASP.NET runtime. See
        ///     http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MotorManageService>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<StorageContext>(opt =>
                {
                    opt.UseSqlServer(_configuration.GetConnectionString("StorageContext"));
                });

            services
                .AddCustomCaching()
                .AddCustomCors()
                .AddCustomOptions(_configuration)
                .AddCustomRouting()
                .AddResponseCaching()
                .AddCustomResponseCompression(_configuration)
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                .AddHttpContextAccessor()
                .AddServerTiming()
                .AddCustomTracing(_configuration)
                .AddControllers();
        }

        /// <summary>
        ///     Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        ///     called by the ASP.NET runtime.
        /// </summary>
        public virtual void Configure(IApplicationBuilder application)
        {
            application.EnsureDatabaseMigrate<StorageContext>();

            application
                .UseIf(
                    _webHostEnvironment.IsDevelopment(),
                    x => x.UseServerTiming())
                .UseForwardedHeaders()
                .UseResponseCaching()
                .UseResponseCompression()
                .UseIf(
                    _webHostEnvironment.IsDevelopment(),
                    x => x.UseDeveloperExceptionPage())
                .UseRouting()
                .UseCors(CorsPolicyName.AllowAny)
                .UseStaticFilesWithCacheControl()
                .UseCustomSerilogRequestLogging()
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapControllers().RequireCors(CorsPolicyName.AllowAny);
                        builder.MapControllers();
                        builder
                            .MapHealthChecks("/status")
                            .RequireCors(CorsPolicyName.AllowAny);
                        builder
                            .MapHealthChecks("/status/self", new HealthCheckOptions {Predicate = _ => false})
                            .RequireCors(CorsPolicyName.AllowAny);
                    })
                .UseOpenApi(c =>
                {
                    // for reverse proxy
                    c.PostProcess = (document, request) =>
                    {
                        if (!new[] { "X-Forwarded-Host", "X-Forwarded-Path" }.All(k => request.Headers.ContainsKey(k)))
                        {
                            return;
                        }
                        document.Host = request.Headers["X-Forwarded-Host"].First();
                        document.BasePath = request.Headers["X-Forwarded-Path"].First();
                    };
                })
                .UseReDoc(c=>c.Path="/docs")
                .UseSwaggerUi3();
        }
    }
}