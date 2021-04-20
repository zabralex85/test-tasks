using NUnit.Framework;
using NSubstitute;
using Medialink.RepositoryInterfaces;
using Medialink.Domain;
using Microsoft.Extensions.Logging;

namespace Medialink.Lib.Tests
{
    [TestFixture]
    public class MathWebClientTests
    {
        private ILoggerRepository _databaseLogger;
        private IApiClientRepository _apiClient;
        private ILogger _logger;

        [SetUp]
        public void SetUp()
        {
            _databaseLogger = Substitute.For<ILoggerRepository>();
            _apiClient = Substitute.For<IApiClientRepository>();
            _logger = Substitute.For<ILogger>();
        }


        [TestCase(4, 5, 9)]
        [TestCase(2, 8, 10)]
        [TestCase(9, 3, 12)]
        [TestCase(3, 2, 5)]
        [TestCase(6, 5, 11)]
        [TestCase(4, 1, 5)]
        public void Add_Inputs_Returns_AppropriateValue(int a, int b, int expectedResult)
        {
            // arrange
            var math = new MathWebClient(_apiClient, _databaseLogger, _logger);

            // arrange
            _apiClient.Get(Arg.Any<string>()).Returns((a + b).ToString());
            _databaseLogger.Log(Arg.Any<RequestLogDbModel>()).Returns(1);

            // act
            var actualResult = math.Add(a, b);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase(4, 5, 20)]
        [TestCase(2, 8, 16)]
        [TestCase(9, 3, 27)]
        [TestCase(3, 2, 6)]
        [TestCase(6, 5, 30)]
        [TestCase(2, 1, 2)]
        public void Multiply_Inputs_Returns_AppropriateValue(int a, int b, int expectedResult)
        {
            // arrange
            var math = new MathWebClient(_apiClient, _databaseLogger, _logger);

            // arrange
            _apiClient.Get(Arg.Any<string>()).Returns((a * b).ToString());
            _databaseLogger.Log(Arg.Any<RequestLogDbModel>()).Returns(1);

            // act
            var actualResult = math.Multiply(a, b);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestCase(25, 5, 5)]
        [TestCase(64, 8, 8)]
        [TestCase(9, 3, 3)]
        [TestCase(4, 2, 2)]
        [TestCase(6, 1, 6)]
        [TestCase(2, 2, 1)]
        public void Divide_Inputs_Returns_AppropriateValue(int a, int b, int expectedResult)
        {
            // arrange
            var math = new MathWebClient(_apiClient, _databaseLogger, _logger);

            // arrange
            _apiClient.Get(Arg.Any<string>()).Returns((a / b).ToString());
            _databaseLogger.Log(Arg.Any<RequestLogDbModel>()).Returns(1);

            // act
            var actualResult = math.Divide(a, b);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [Test]
        public void Test_AddMethodLogsSomeValue_Called()
        {
            // arrange
            var math = new MathWebClient(_apiClient, _databaseLogger, _logger);

            // arrange
            _apiClient.Get(Arg.Any<string>()).Returns((1 + 2).ToString());

            // act
            _ = math.Add(1, 2);

            // assert
            _databaseLogger.ReceivedWithAnyArgs().Log(default);
        }
    }
}
