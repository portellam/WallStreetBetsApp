using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;
using BackEnd.Models;
using System.Linq;

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





        // ======================================================================================================
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
        // ======================================================================================================





        // ======================================================================================================
        // THESE ARE THE GET, POST, AND DELETE FOR THE FAVORITES TABLE
        [Route("favorites")]
        [HttpGet]
        public IEnumerable<Favorite> GetFavorites()
        {
            return _context.Favorites;
        }

        [Route("favorites")]
        [HttpPost]
        public Favorite AddFav(string username, string ticker)
        {
            List<Favorite> Favs = _context.Favorites.ToList();
            for (int i = 0; i < Favs.Count; i++)
            {
                if (username == Favs[i].username && ticker == Favs[i].ticker)
                {
                    return null; // If Favorite already exists, exit function
                }
            }
            Favorite newFav = new Favorite();
            newFav.username = username;
            newFav.ticker = ticker;
            _context.Favorites.Add(newFav);
            _context.SaveChanges();
            return newFav;
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
        // ======================================================================================================





        // ======================================================================================================
        // THESE ARE THE CRUD OPERATIONS FOR OUR NOTES TABLE
        [Route("notes")]
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes;
        }

        [Route("notes")]
        [HttpPost]
        public void AddNote(int favID, string noteDescription)
        {
            Note myNote = new Note();
            List<Favorite> favoriteRecords = _context.Favorites.ToList();

            for (int i = 0; i < favoriteRecords.Count; i++)
            {
                if (favoriteRecords[i].id == favID)
                {
                    myNote.favorite_id = favID;
                    myNote.description = noteDescription;

                    _context.Notes.Add(myNote);
                    _context.SaveChanges();
                }
            }
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~ THIS IS A PIECE OF CODE THAT UPDATES OUR DATABASE ~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        [Route("notes")]
        [HttpPut]
        public void EditNote(int noteID, string updatedNoteDescription)
        {
            List<Note> notesList = _context.Notes.ToList();
            Note myNote = new Note();

            for (int i = 0; i < notesList.Count; i++)
            {
                if (notesList[i].id == noteID)
                {
                    notesList[i].description = updatedNoteDescription;
                    myNote = notesList[i];

                    _context.Notes.Update(myNote);
                    _context.SaveChanges();
                }
            }
        }

        [Route("notes")]
        [HttpDelete]
        public void DeleteNote(int noteID)
        {
            List<Note> notesList = _context.Notes.ToList();
            Note noteToDelete = new Note();

            for (int i = 0; i < notesList.Count; i++)
            {
                if (notesList[i].id == noteID)
                {
                    noteToDelete = notesList[i];

                    _context.Notes.Remove(noteToDelete);
                    _context.SaveChanges();
                }
            }
        }
        // ======================================================================================================


        




        // EXPERIMENTATION FOR JOINRESULTS
        // SEEMS LIKE IT'S WORKING, BUT I AM STILL UNSURE IF "ID" IS CORRECT
        // EXAMPLE URL: https://localhost:7262/api/WallStreetBets/joinresults?username=coloritoj

        [Route("joinresults")]
        [HttpGet]
        public IEnumerable<JoinResults> GetJoins(string username)
        {
            List<JoinResults> myJoinResults = WallStreetBetsDB.GetJoinResults(username);
            return myJoinResults;
        }



        // =================================================================
        // THIS IS JEFF'S CODE HERE:

        [Route("nbshare")]
        [HttpGet]
        public async Task<string> get()
        {

            // IMPORTANT:
            // Notice I'm not bothering with making classes for the response. Ultimately all
            // http requests are really just strings anyway. So I'm just reading the JSON as
            // a string and passing it exactly as-is back out. The Angular app in turn will
            // correctly read it as JSON. (Interestingly, the ASP.NET core seems to notice
            // that the data is JSON and attaches the correct content type of application/json.)

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://dashboard.nbshare.io/api/v1/");
            HttpResponseMessage response = await client.GetAsync("apps/reddit");
            string json = await response.Content.ReadAsStringAsync();
            return json;
        }

        [Route("marketstack")]
        [HttpGet]
        public async Task<MarketStackObject> getMarketStackInfo(string ticker)
        {
            HttpClient client = new HttpClient();
            //    http://api.marketstack.com/v1/eod?access_key=208302dbe2d07c780ba4de2dc30c56ba&symbols=DIS&limit=1
            client.BaseAddress = new Uri("http://api.marketstack.com/v1/");
            // Just added new API key below on 2/11/2022
            var connection = await client.GetAsync($"eod?access_key=a6b0ab8551d6ead2bb1df2da121ff9d9&symbols={ticker}&limit=1");
            MarketStackObject marketStackObject = await connection.Content.ReadAsAsync<MarketStackObject>();
            return marketStackObject;
        }
        
    }
}
