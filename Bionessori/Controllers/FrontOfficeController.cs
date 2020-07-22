using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bionessori.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FrontOfficeController : ControllerBase {
        // GET: api/<FrontOfficeController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FrontOfficeController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<FrontOfficeController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<FrontOfficeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<FrontOfficeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
