using System;
using Autofac;
using Medialink.Lib;
using Serilog.Core;

namespace Medialink.Console
{
    internal enum Operation
    {
        Add,
        Multiply,
        Divide
    }

    internal static class Program
    {
        private static IContainer _container;
        private static IMathWebClient _client;
        private static ILifetimeScope _scope;
        private static Logger _logger;

        private static int[] RandomInts(Random rand, int count, int min, int max)
        {
            int[] numArray = new int[count];
            for (int index = 0; index < count; index++)
            {
                numArray[index] = rand.Next(min, max);
            }

            return numArray;
        }

        private static void Main(string[] args)
        {
            _logger = new Serilog.LoggerConfiguration().CreateLogger();
            _container = ContainerConfig.Configure();
            _scope = _container.BeginLifetimeScope();
            _client = _scope.Resolve<IMathWebClient>();

            var random = new Random();
            int[] aData = RandomInts(random, 100, 10, 20);
            int[] bData = RandomInts(random, 100, 10, 20);

            try
            {
                for (int i = 0; i < aData.Length; i++)
                {
                    int? sum = GetResult(Operation.Add, aData[i], bData[i]);
                    _logger.Information("SUM of {0},{1}:{2}", aData[i], bData[i], sum);

                    int? multiply = GetResult(Operation.Multiply, aData[i], bData[i]);
                    _logger.Information("Multiply of {0},{1}:{2}", aData[i], bData[i], multiply);

                    int? divide = GetResult(Operation.Divide, aData[i], bData[i]);
                    _logger.Information("Divide of {0},{1}:{2}", aData[i], bData[i], divide);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            _scope.Dispose();
            _container.Dispose();

            System.Console.Read();
        }

        private static int? GetResult(Operation operation, int a, int b)
        {
            try
            {
                switch (operation)
                {
                    case Operation.Add:
                        return _client.Add(a, b);
                    case Operation.Multiply:
                        return _client.Multiply(a, b);
                    case Operation.Divide:
                        return _client.Divide(a, b);
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
