using Autofac;
using System;
using Medialink.Database;
using Medialink.ApiClient;
using Medialink.RepositoryInterfaces;
using Serilog;

namespace Medialink.Lib
{
    public static class ContainerConfig
    {
        public static IContainer Configure(string baseUrl = "http://localhost:12345/", LogMode logMode = LogMode.Console, string connectionString = null)
        {
            if(string.IsNullOrEmpty(baseUrl))
                throw new ArgumentException("Url is not configured");

            if(logMode == LogMode.DataBase && string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("InMemoryMode not chosen or connection string for db is not present");

            try
            {
                var builder = new ContainerBuilder();

                var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
                var loggerConfig = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();
                loggerFactory.AddSerilog(loggerConfig);

                var logger = loggerFactory.CreateLogger("Medialink-Logger");

                builder.RegisterInstance(logger).As<Microsoft.Extensions.Logging.ILogger>();

                builder.RegisterType<MathWebClient>().As<IMathWebClient>();
                builder.RegisterInstance(new RestSharpClient(baseUrl)).As<IApiClientRepository>();

                if (logMode == LogMode.InMemory)
                {
                    var memoryContext = new InMemoryHelper();
                    builder.RegisterInstance(new InMemoryLogger(memoryContext, logger)).As<ILoggerRepository>();
                }

                if (logMode == LogMode.Console)
                {
                    builder.RegisterInstance(new ConsoleLogger(logger)).As<ILoggerRepository>();
                }

                if (logMode == LogMode.DataBase)
                {
                    var databaseContext = new SqlDatabaseDapperHelper(connectionString);
                    builder.RegisterInstance(new SqlDatabaseDapperLogger(databaseContext, logger)).As<ILoggerRepository>();
                }

                var container = builder.Build();
                return container;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public enum LogMode
    {
        InMemory = 1,
        Console = 2,
        DataBase = 3
    }
}
