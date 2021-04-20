using Medialink.Domain;
using Medialink.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Medialink.Database
{
    public class InMemoryLogger : ILoggerRepository
    {
        private readonly InMemoryHelper _memoryHelper;
        private readonly ILogger _logger;

        public InMemoryLogger(InMemoryHelper memoryHelper, ILogger logger)
        {
            _memoryHelper = memoryHelper;
            _logger = logger;
        }

        private int Insert(RequestLogDbModel requestLog)
        {
            _memoryHelper.Insert(requestLog);
            return 1;
        }

        public int Log(RequestLogDbModel requestLog)
        {
            PrintLogDetailsInConsole(requestLog);

            int rowsAffected = Insert(requestLog);
            return rowsAffected;
        }

        private void PrintLogDetailsInConsole(RequestLogDbModel requestLog)
        {
            _logger.LogInformation("LogMethod:{LogMethod} / IsSuccess:{IsSuccess} / Timestamp:{Timestamp}",
                requestLog.LogMethod,
                requestLog.IsSuccess,
                requestLog.Timestamp);
        }
    }
}