using Medialink.Domain;
using Medialink.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Medialink.Database
{
    public class ConsoleLogger : ILoggerRepository
    {
        private readonly ILogger _logger;

        public ConsoleLogger(ILogger logger)
        {
            _logger = logger;
        }

        public int Log(RequestLogDbModel requestLog)
        {
            _logger.LogInformation("LogMethod:{LogMethod} / IsSuccess:{IsSuccess} / Timestamp:{Timestamp}",
                requestLog.LogMethod,
                requestLog.IsSuccess, 
                requestLog.Timestamp);

            return 1;
        }
    }
}