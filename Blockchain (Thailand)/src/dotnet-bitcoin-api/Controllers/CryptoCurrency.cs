using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_bitcoin_api.business_layer;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_bitcoin_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoCurrencyController : ControllerBase
    {
        private Finance Finance { get; set; }
        // private readonly IEmployeeRepository _employeeRepo;


        public CryptoCurrencyController(short rpcRequestTimeoutInSeconds = 10, bool useTestnet = true)
        {
            Finance = new Finance(rpcRequestTimeoutInSeconds, useTestnet);
        }

        [Route("send_btc")]
        [HttpPost]
        public void SendBtc([FromBody] Models.SendQuery query)
        {
        }

        [Route("get_last")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetLast()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("info")]
        [HttpGet]
        public ActionResult<decimal> GetInfo()
        {
            return Finance.GetBalance();
        }
    }
}
