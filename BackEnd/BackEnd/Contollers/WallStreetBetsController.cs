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
        // Database
	private readonly WallStreetBetsContext _context;

        // METHODS //
	public WallStreetBetsController(WallStreetBetsContext context)
	{
		_context = context;
	}
		
	// CRUD FUNCTIONS
	
        // User table
	
	// function reads the list of Users.
        [HttpGet]
	public IEnumerable<User> GetUsers()
	{
		return _context.Users;
	}
	
	// function edits User, checks for conflicting match.
	[HttpPost]
	public void PostUser(string username, string first_name)
	{
		User myUser = new User();
		myUser.username = username;
		myUser.first_name = first_name;
		_context.Users.Add(myUser);
		_context.SaveChanges();
	        // URL: https://localhost:7262/api/WallStreetBets?username=jeffcogs&first_name=jeff
	}
	/*

	// function edits User, checks for conflicting match.
        [HttpPut]
        public void EditUser(int id, string username, string first_name)
        {
            // FrontEnd:
            // verify input, if string is null or same, assume no change and pass old string
            // else, pass new string
            List<User> userList = _context.Users.ToList();
            for (int i = 0; i < userList.Count; i++)
            {
                if (id != i && username == userList[i].username)
                {
                    return;     // If user is not current user and match exists, exit function now.
                }
            }
            User myUser = userList.ElementAt(id);
            myUser.username = username;
            myUser.firstName = first_name;
            _context.Users.Update(myUser);
            _context.SaveChanges();
        }
	*/

        // function deletes a User.
	/*
        [HttpDelete]
        public void DeleteUser(int id)
        {
            List<User> userList = _context.Users.ToList();
            for (int i = 0; i < userList.Count; i++)
            {
                if (id == userList[i].id)
                {
                    // TODO: recursive call DeleteNote(), and DeleteFavorite() here?
                    _context.Users.Remove(userList.ElementAt(id));
                    _context.SaveChanges();
                    return;     // If match exists, exit function now.
                }
            }
        }
	*/

        // ==================================================================================================== //

        // Favorite table CRUD
        // NOTE: Put/Edit is not necessary for Favorite
	
	// function reads list of Favorites.
        [Route("favorites")]
        [HttpGet]
        public IEnumerable<Favorite> GetFavorites()
        {
            return _context.Favorites;
        }

	// function creates Favorite, assigns to a User.
        [Route("favorites")]
        [HttpPost]
        public void AddFav(string username, string ticker)
        {
            List<Favorite> Favs = _context.Favorites.ToList();
            for (int i = 0; i < Favs.Count; i++)
            {
                if (username == Favs[i].username && ticker == Favs[i].ticker)
                {
                    return; // If match exists, exit function now.
                }
            }
            Favorite newFav = new Favorite();
            newFav.username = username;
            newFav.ticker = ticker;
            _context.Favorites.Add(newFav);
            _context.SaveChanges();
        }
	
	// function deletes Favorite.
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
                    return; // If match exists, delete favorite, and exit function now.
                }
            }
        }
        // ==================================================================================================== //

        // Note table CRUD

	// function reads list of Notes.
        [Route("notes")]
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes;
        }

	// function creates Note, assigns to Favorite Ticker.
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
                    return; // If match exists, add note, and exit function now.
                }
            }
        }

	// function edits Note.
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
                    return; // If match exists, edit note, and exit function now.
                }
            }
        }

	// function deletes Note.
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
                    return; // If match exists, delete note, and exit function now.
                }
            }        
        }
        // ==================================================================================================== //

        // JoinResults table
        // TODO: appears to be working, unsure if "id" is correct
        // URL: https://localhost:7262/api/WallStreetBets/joinresults?username=coloritoj
	
	// function reads list of JoinResults.
        [Route("joinresults")]
        [HttpGet]
        public IEnumerable<JoinResults> GetJoins(string username)
        {
            List<JoinResults> myJoinResults = WallStreetBetsDB.GetJoinResults(username);
            return myJoinResults;
        }
        // ==================================================================================================== //
        
        // Web API
        // NOTE: Jeff's code here
	
	// nbshare custom API for reddit.com/r/WallStreetBets
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

	// MarketStack API
        [Route("marketstack")]
        [HttpGet]
        public async Task<MarketStackObject> getMarketStackInfo()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://api.marketstack.com/v1/");
            var connection = await client.GetAsync("eod?access_key=208302dbe2d07c780ba4de2dc30c56ba&symbols=GME&limit=1");  // NOTE: Need to change parameters, this is just currently testing 1 record of GME
            MarketStackObject marketStackObject = await connection.Content.ReadAsAsync<MarketStackObject>();
            return marketStackObject;
        }
    }
}
