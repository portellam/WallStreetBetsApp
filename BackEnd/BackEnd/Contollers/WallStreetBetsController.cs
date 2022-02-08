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
		// THIS IS OUR DATABASE
		private readonly WallStreetBetsContext _context;
		public WallStreetBetsController(WallStreetBetsContext context)
		{
			_context = context;
		}



        // THESE ARE THE GET & POST FOR THE USERS TABLE
		[HttpGet]
		public IEnumerable<User> GetUsers()
		{
			return _context.Users;
		}

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




        // THESE ARE THE GET, POST, AND DELETE FOR THE FAVORITES TABLE
        [Route("favorites")]
        [HttpGet]
        public IEnumerable<Favorite> GetFavorites()
        {
            return _context.Favorites;
        }

        [Route("favorites")]
        [HttpPost]
        public void AddFav(string username, string ticker)
        {
            List<Favorite> Favs = _context.Favorites.ToList();
            for (int i = 0; i < Favs.Count; i++)
            {
                if (username == Favs[i].username && ticker == Favs[i].ticker)
                {
                    return; // If Favorite already exists, exit function
                }
            }
            Favorite newFav = new Favorite();
            newFav.username = username;
            newFav.ticker = ticker;
            _context.Favorites.Add(newFav);
            _context.SaveChanges();
        }

        [Route("favorites")]
        [HttpDelete]
        public void DeleteFav(string username, string ticker)
        {
            List<Favorite> Favs = _context.Favorites.ToList();
            for (int i = 0; i < Favs.Count; i++)
            {
                if (username == Favs[i].username && ticker == Favs[i].ticker)
                {
                    _context.Favorites.Remove(Favs[i]);
                    _context.SaveChanges();
                }
            }

        }




        // THESE ARE THE CRUD OPERATIONS FOR OUR NOTES TABLE

        [Route("notes")]
        [HttpPost]
        public void AddNote(string description)
        {
            Note theNote = new Note();
            List<Favorite> theFavoriteList = _context.Favorites.ToList();
        }
        // fsdfjsadkl;fjsakl;dj




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
        /*
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
        */

        // function creates Favorite Ticker, assigns to a User
        // POST api/<WallStreetBetsController>

        /*
        
        [HttpGet]
        public IEnumerable<User> GetFavs()
        {
            return _context.Favorites;
        }
        */







    }
}
