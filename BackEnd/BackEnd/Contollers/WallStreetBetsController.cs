using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;
using BackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallStreetBetsController : ControllerBase
    {
        // PROPERTIES //
        private readonly WallStreetBetsContext _context;
        public WallStreetBetsController(WallStreetBetsContext context)
        {
            _context = context;
        }

        // METHODS //
        // CRUD FUNCTIONS

        // TODO:    Users table

        // function reads the list of Users
        // GET: api/<WallStreetBetsController>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // function creates a User, adds to list
        [HttpPost]
        public void PostUser(string username, string first_name)
        {
            // we are taking in values, which is a username and first_name, and adding to list
            // do I instantiate a list of users here, or call _context.Users ?

            User myUser = new User();
            myUser.username = username;
            myUser.first_name = first_name;

            _context.Users.Add(myUser);
            _context.SaveChanges();

            // EXAMPLE: https://localhost:7262/api/WallStreetBets?username=jeffcogs&first_name=jeff
        }

        /*
        [HttpPut]
        public void EditUser(string currentUsername, string newUsername, string newFirstName)
        {
            
        }
        */



        /*

        // function deletes a User (also should delete all Favs/Notes of given User, too)
        [HttpGet]
        public void DeleteUser(int id, List<User> Users)
        {
            // foreach or forloop?
            for (int i = 0; i < Users.Count; i++)
            {
                if (i == id)
                {
                    // delete user here
                }
            }
            return;
        }
        */


        // TODO:    Favorites table

        // function reads Favorite Ticker
        // GET api/<WallStreetBetsController>/5
        [HttpGet("{id}")]
        public static List<Favorite> GetFavs(string username)
        {
            List<Favorite> result = null;
            using (WallStreetBetsContext ctx = new WallStreetBetsContext())
            {
                var query = 
            }
        }




        /*


        // function creates Favorite Ticker, assigns to a User
        // POST api/<WallStreetBetsController>
        [HttpPost]
        public void PostFav(string username, string ticker)
        {
            

        }

        // function updates Favorite Ticker
        // PUT api/<WallStreetBetsController>/5
        [HttpPut("{id}")]
        public void PutFav(string username, string ticker, List<Favorite> Favorites)
        {

        }

        // function deletes Favorite Ticker
        // DELETE api/<WallStreetBetsController>/5
        [HttpDelete("{id}")]
        public void DeleteFav(string username, string ticker, List<Favorite> Favorites)
        {

        }


       
        */

    }
}
