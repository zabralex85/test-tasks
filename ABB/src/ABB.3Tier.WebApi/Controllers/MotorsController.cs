using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ABB.NTier.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorsController : ControllerBase
    {
        // GET: api/<MotorsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MotorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MotorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MotorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MotorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
