using Medialink.Domain;
using Medialink.RepositoryInterfaces;
using Microsoft.Extensions.Logging;

namespace Medialink.Database
{
    public class SqlDatabaseDapperLogger : ILoggerRepository
    {
        private readonly SqlDatabaseDapperHelper _databaseConnection;
        private readonly ILogger _logger;

        public SqlDatabaseDapperLogger(SqlDatabaseDapperHelper databaseConnection, ILogger logger)
        {
            _databaseConnection = databaseConnection;
            _logger = logger;
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

        private int Insert(RequestLogDbModel requestLog)
        {
            string query = "INSERT INTO dbo.Log VALUES (@ParamLogMethod, @ParamIsSuccess @ParamTimestamp); ";

            var parameters = new
            {
                ParamLogMethod = requestLog.LogMethod,
                ParamIsSuccess = requestLog.IsSuccess,
                ParamTimestamp = requestLog.Timestamp
            };

            int rowsAffected = _databaseConnection.Execute(query, parameters);
            return rowsAffected;
        }
    }
}
