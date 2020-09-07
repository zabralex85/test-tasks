using Medialink.RepositoryInterfaces;
using System;
using Medialink.Domain;
using Microsoft.Extensions.Logging;

namespace Medialink.Lib
{
    public class MathWebClient : IMathWebClient
    {
        private readonly IApiClientRepository _apiClient;
        private readonly ILoggerRepository _loggerRepository;
        private readonly ILogger _logger;

        public MathWebClient(IApiClientRepository apiClient, ILoggerRepository loggerRepository, ILogger logger)
        {
            _apiClient = apiClient;
            _loggerRepository = loggerRepository;
            _logger = logger;
        }

        public int Add(int a, int b)
        {
            string route = $"api/math/Add/{a}/{b}";

            try
            {
                string responseStr = _apiClient.Get(route);
                int sum = Convert.ToInt32(responseStr);

                LogRequest(route, true);

                return sum;
            }
            catch (Exception)
            {
                LogRequest(route, false);
                throw;
            }
        }

        public int Multiply(int a, int b)
        {
            string route = $"api/math/Multiply/{a}/{b}";

            try
            {
                string responseStr = _apiClient.Get(route);
                int product = Convert.ToInt32(responseStr);

                LogRequest(route, true);

                return product;
            }
            catch (Exception)
            {
                LogRequest(route, false);
                throw;
            }
        }

        public int Divide(int a, int b)
        {
            string route = $"api/math/Divide/{a}/{b}";

            try
            {
                string responseStr = _apiClient.Get(route);
                int quotient = Convert.ToInt32(responseStr);

                LogRequest(route, true);

                return quotient;
            }
            catch (Exception)
            {
                LogRequest(route, false);
                throw;
            }
        }

        private void LogRequest(string route, bool isSuccess)
        {
            RequestLogDbModel requestLog = new RequestLogDbModel
            {
                LogMethod = route,
                IsSuccess = isSuccess,
                Timestamp = DateTime.Now
            };

            try
            {
                int rowsAffected = _loggerRepository.Log(requestLog);

                PrintLogInsertResultInConsole(rowsAffected, null);
            }
            catch (Exception ex)
            {
                PrintLogInsertResultInConsole(-1, ex.Message);
                throw;
            }

        }

        private void PrintLogInsertResultInConsole(int rowsAffected, string errorMessage)
        {
            if (rowsAffected > 0)
            {
                _logger.LogInformation($"LOG ADDED TO DATABASE SUCCESSFULLY");
            }
            else
            {
                _logger.LogInformation($"ADD LOG TO DATABASE FAILED :( ERROR MESSAGE: {errorMessage})", errorMessage);
            }
        }

    }
}
