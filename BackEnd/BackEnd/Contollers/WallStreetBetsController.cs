using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;
using BackEnd.Models;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// NOTE: changed method/temporary variable names to another pattern, from is seen in professional environments.

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
		public void PostUser(string _username, string _first_name)
		{
			User _User = new User();
			_User.username = _username;
			_User.first_name = _first_name;
			_context.Users.Add(_User);
			_context.SaveChanges();
			// URL: https://localhost:7262/api/WallStreetBets?username=jeffcogs&first_name=jeff
		}
		
		/*
        // function edits User, checks for conflicting match.
            [HttpPut]
            public void EditUser(int _id, string _username, string _first_name)
            {
                // FrontEnd:
                // verify input, if string is null or same, assume no change and pass old string
                // else, pass new string
                List<User> _Users = _context.Users.ToList();
                for (int i = 0; i < _Users.Count; i++)
                {
                    if (_id != i && _username == _Users[i].username)
                    {
                        return;     // If user is not current user and match exists, exit function now.
                    }
                }
                User _User = _Users.ElementAt(_id);
                _User.username = _username;
                _User.first_name = _first_name;
                _context.Users.Update(_User);
                _context.SaveChanges();
            }
        */

		/*
		// function deletes a User.
            [HttpDelete]
            public void DeleteUser(int _id)
            {
                List<User> _Users = _context.Users.ToList();
                for (int i = 0; i < _Users.Count; i++)
                {
                    if (_id == _Users[i].id)
                    {
                        // TODO: recursive call DeleteNote(), and DeleteFavorite() here?
                        _context.Users.Remove(_Users.ElementAt(_id));
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
		public void AddFav(string _username, string _ticker)
		{
			List<Favorite> _Favorites = _context.Favorites.ToList();
			Favorite _Favorite = new Favorite();
			for (int i = 0; i < _Favorites.Count; i++)
			{
				if (_username == _Favorites[i].username && _ticker == _Favorites[i].ticker)
				{
					return; // If match exists, exit function now.
				}
			}
			_Favorite.username = _username;
			_Favorite.ticker = _ticker;
			_context.Favorites.Add(_Favorite);
			_context.SaveChanges();
		}

		// function deletes Favorite.
		[Route("favorites")]
		[HttpDelete]
		public void DeleteFav(string _username, string _ticker)
		{
			List<Favorite> _Favorites = _context.Favorites.ToList();
			for (int i = 0; i < _Favorites.Count; i++)
			{
				if (_username == _Favorites[i].username && _ticker == _Favorites[i].ticker)
				{
					_context.Favorites.Remove(_Favorites[i]);
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
		public void AddNote(int _favorite_id, string _description)
		{
			List<Favorite> _Favorites = _context.Favorites.ToList();
			Note _Note = new Note();
			for (int i = 0; i < _Favorites.Count; i++)
			{
				if (_Favorites[i].id == _favorite_id)
				{
					_Note.description = _description;
					_Favorites[i].id = _favorite_id;
					_context.Notes.Add(_Note);
					_context.SaveChanges();
					return; // If match exists, add note, and exit function now.
				}
			}
		}

		// function edits Note.
		[Route("notes")]
		[HttpPut]
		public void EditNote(int _id, string _description)
		{
			List<Note> _Notes = _context.Notes.ToList();
			for (int i = 0; i < _Notes.Count; i++)
			{
				if (_id == _Notes[i].id)
				{
					Note _Note = _Notes[i];
					_Note.description = _description;
					_context.Notes.Update(_Note);
					//_Notes[i].description = _description;
					//_context.Notes.Update(_Notes[i]);		// TODO: change to this?
					_context.SaveChanges();
					return; // If match exists, edit note, and exit function now.
				}
			}
		}

		// function deletes Note.
		[Route("notes")]
		[HttpDelete]
		public void DeleteNote(int _id)
		{
			List<Note> _Notes = _context.Notes.ToList();
			for (int i = 0; i < _Notes.Count; i++)
			{
				if (_id == _Notes[i].id)
				{
					//Note _Note = notesList[i];
					//_context.Notes.Remove(_Note);
					_context.Notes.Remove(_Notes[i]);
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
		public IEnumerable<JoinResults> GetJoins(string _username)
		{
			List<JoinResults> _JoinResults = WallStreetBetsDB.GetJoinResults(_username);
			return _JoinResults;
		}
		// ==================================================================================================== //

		// Web API
		// NOTE: Jeff's code here

		// Nbshare API for reddit.com
		[Route("nbshare")]
		[HttpGet]
		//public async Task<string> GetNbshare		// TODO: change to this?
		public async Task<string> get()
		{
			// IMPORTANT:
			// Notice I'm not bothering with making classes for the response. Ultimately all
			// http requests are really just strings anyway. So I'm just reading the JSON as
			// a string and passing it exactly as-is back out. The Angular app in turn will
			// correctly read it as JSON. (Interestingly, the ASP.NET core seems to notice
			// that the data is JSON and attaches the correct content type of application/json.)

			HttpClient _HttpClient = new HttpClient();
			_HttpClient.BaseAddress = new Uri("https://dashboard.nbshare.io/api/v1/");
			HttpResponseMessage response = await _HttpClient.GetAsync("apps/reddit");
			string json = await response.Content.ReadAsStringAsync();
			return json;
		}

		// MarketStack API
		[Route("marketstack")]
		[HttpGet]
		//public async Task<MarketStackObject> GetMarketStackInfo()		// TODO: change to this?
		public async Task<MarketStackObject> getMarketStackInfo()
		{
			HttpClient _HttpClient = new HttpClient();
			_HttpClient.BaseAddress = new Uri("http://api.marketstack.com/v1/");
			var connection = await _HttpClient.GetAsync("eod?access_key=208302dbe2d07c780ba4de2dc30c56ba&symbols=GME&limit=1");  // NOTE: Need to change parameters, this is just currently testing 1 record of GME
			MarketStackObject marketStackObject = await connection.Content.ReadAsAsync<MarketStackObject>();
			return marketStackObject;
		}
	}
}
