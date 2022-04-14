using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RentalsAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseListingsController : ControllerBase
    {
        // GET: api/<HouseListingsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HouseListingsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HouseListingsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HouseListingsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HouseListingsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
