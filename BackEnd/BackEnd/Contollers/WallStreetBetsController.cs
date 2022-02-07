using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallStreetBetsController : ControllerBase
    {

        private readonly WallStreetBetsContext _context;
        public WallStreetBetsController(WallStreetBetsContext context)
        {
            _context = context;
        }

        // GET: api/<WallStreetBetsController>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET api/<WallStreetBetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "JoshC";
        }

        // POST api/<WallStreetBetsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WallStreetBetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WallStreetBetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
