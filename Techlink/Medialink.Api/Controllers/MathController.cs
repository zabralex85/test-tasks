using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Medialink.Api.Controllers
{
    public class MathController : ApiController
    {
        [HttpGet]
        [Route("api/math/Add/{a}/{b}")]
        public HttpResponseMessage Add(int a, int b)
        {
            HttpResponseMessage response;

            try
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Convert.ToString(a + b))
                };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return response;
        }

        [HttpGet]
        [Route("api/math/Multiply/{a}/{b}")]
        public HttpResponseMessage Multiply(int a, int b)
        {
            HttpResponseMessage response;

            try
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Convert.ToString(a * b))
                };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return response;
        }

        [HttpGet]
        [Route("api/math/Divide/{a}/{b}")]
        public HttpResponseMessage Divide(int a, int b)
        {
            HttpResponseMessage response;

            try
            {
                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(Convert.ToString(a / b))
                };
            }
            catch (Exception ex)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
            }

            return response;
        }
    }
}
