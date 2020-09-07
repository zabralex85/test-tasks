using NUnit.Framework;
using Medialink.Api.Controllers;
using System.Net;
using System.Web.Http;
using System.Net.Http;

namespace Medialink.Api.Tests
{
    [TestFixture]
    public class MathControllerTests
    {
        [Test]
        public void Add_FourAndFive_Returns_Nine()
        {
            // arrange 
            var controller = GetMathController("api/math/Add/");

            // act 
            var response = controller.Add(4, 5).Content.ReadAsStringAsync().Result;

            // assert
            Assert.AreEqual("9", response);
        }



        [Test]
        public void Multiply_FourAndFive_Returns_Twenty()
        {
            // arrange 
            var controller = GetMathController("api/math/multiply/");

            // act 
            var response = controller.Multiply(4, 5).Content.ReadAsStringAsync().Result;

            // assert
            Assert.AreEqual("20", response);
        }



        [Test]
        public void Divide_FourAndTwo_Returns_Two()
        {
            // arrange 
            var controller = GetMathController("api/math/divide/");

            // act 
            var response = controller.Divide(4, 2).Content.ReadAsStringAsync().Result;

            // assert
            Assert.AreEqual("2", response);
        }



        [Test]
        public void Divide_ByZero_Returns_500Error()
        {
            // arrange 
            var controller = GetMathController("api/math/divide/");

            // act 
            var response = controller.Divide(3, 0).StatusCode;

            // assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response);
        }



        // Helpers

        private static MathController GetMathController(string route)
        {
            var result = new HttpRouteCollection(route);

            var controller = new MathController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration(result)
            };

            return controller;
        }
    }
}
