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
        // PROPERTIES //

        // Database
        private readonly WallStreetBetsContext _context;
        // ==================================================================================================== //

        // METHODS //
        public WallStreetBetsController(WallStreetBetsContext context)
        {
            _context = context;
        }

        // CRUD FUNCTIONS

        // User table
        // NOTE: This is a piece of code that retrieves info from our database.

        // function reads the list of Users.
        [HttpGet]
		public IEnumerable<User> GetUsers()
		{
			return _context.Users;
		}

        // function adds User.
        [HttpPost]
        //public void AddUser(string _username, string _first_name)		// TODO: change?
        public void PostUser(string _username, string _first_name)
        {
            User _User = new User();
            _User.username = _username;
            _User.first_name = _first_name;

            _context.Users.Add(_User);
            _context.SaveChanges();
            // URL: https://localhost:7262/api/WallStreetBets?username=jeffcogs&first_name=jeff
        }

        // function edits User.
        [HttpPut]
        public void EditUser(int _id, string _username, string _first_name)     // NOTE: WORKS!
        {
            List<User> _Users = _context.Users.ToList();

            // subfunction checks for change in User.
            if (_username == _Users[_id].username && _first_name == _Users[_id].first_name)
            {
                return; // If no change, exit function now.
            }

            // subfunction verifies input.
            for (int i = 0; i < _Users.Count; i++)
            {
                if (_id == _Users[i].id)
                {
                    _Users[_id].username = _username;
                    _Users[_id].first_name = _first_name;

                    _context.Users.Update(_Users[_id]);
                    _context.SaveChanges();
                    return; // If match exists, edit user, and exit function now.
                }
            }
        }

        // function deletes a User.		// NOTE: NOT tested!
        [HttpDelete]
        public void DeleteUser(int _id)
        {
            List<User> _Users = _context.Users.ToList();

            for (int i = 0; i < _Users.Count; i++)
            {
                if (_id == _Users[i].id)
                {
                    User _User = _Users[i];

                    _context.Users.Remove(_User);
                    _context.SaveChanges();
                    return;     // If match exists, delete user, and exit function now.
                }
            }
        }
        // ==================================================================================================== //

        // Favorite table
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
        public Favorite AddFav(string _username, string _ticker)
        {
            List<Favorite> _Favorites = _context.Favorites.ToList();

            for (int i = 0; i < _Favorites.Count; i++)
            {
                if (_username == _Favorites[i].username && _ticker == _Favorites[i].ticker)
                {
                    return null; // If match exists, exit function now.
                }
            }
            Favorite _Favorite = new Favorite();
            _Favorite.username = _username;
            _Favorite.ticker = _ticker;
            //_Favorite.note_ids = new List<int>();

            _context.Favorites.Add(_Favorite);
            _context.SaveChanges();
            return _Favorite;
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

        // Note table
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

            for (int i = 0; i < _Favorites.Count; i++)
            {
                if (_Favorites[i].id == _favorite_id)
                {
                    Note _Note = new Note();
                    _Note.description = _description;
                    //_Note.lastEdit = DateTime.Now;
                    _Favorites[i].id = _favorite_id;
                    //_Favorites[i].note_ids.Add(i);	// add current Note to List in Favorite

                    _context.Notes.Add(_Note);
                    _context.SaveChanges();
                    return; // If match exists, add note, and exit function now.
                }
            }
        }

        // function edits Note.
        // NOTE: this function updates our local database.
        [Route("notes")]
        [HttpPut]
        public void EditNote(int _id, string _description)
        {
            List<Note> _Notes = _context.Notes.ToList();

            // subfunction checks for change in Note.
            // NOTE: NOT tested! unnecessary?
            /*
			if (_description == _Notes[_id].description)
			{
				return; // If no change, exit function now.
			}
			// subfunction verifies input.
			else
			*/
            {
                for (int i = 0; i < _Notes.Count; i++)
                {
                    if (_id == _Notes[i].id)
                    {
                        _Notes[i].description = _description;
                        //_Notes[i].lastEdit = DateTime.Now;

                        _context.Notes.Update(_Notes[i]);
                        _context.SaveChanges();
                        return; // If match exists, edit note, and exit function now.
                    }
                }
            }
        }
    

        // function deletes Note.
        [Route("notes")]
        [HttpDelete]
        public void DeleteNote(int _id)
        {
            List<Favorite> _Favorites = _context.Favorites.ToList();
            List<Note> _Notes = _context.Notes.ToList();

            for (int i = 0; i < _Notes.Count; i++)
            {
                if (_id == _Notes[i].id)
                {
                    _context.Notes.Remove(_Notes[i]);
                    _context.SaveChanges();
                    return; // If match exists, delete note, and exit function now.
                }
            }
        }
        // ==================================================================================================== //

        // JoinResults table
        // TODO: appears to be working, unsure if "id" is correct.
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
        // Nbshare API (for reddit.com)
        [Route("nbshare")]
        [HttpGet]
        //public async Task<string> GetNbshare		// TODO: change?
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

        // PROPERTIES

        // MarketStack
        // NOTE: these free API keys each last 100 req/mo.
        private int MarketStackKeys_index = 0;
        private List<string> MarketStackKeys = new List<string>{
            "208302dbe2d07c780ba4de2dc30c56ba",	// Josh
			"a6b0ab8551d6ead2bb1df2da121ff9d9", // Josh
			"289ebff0ebb79e4eb51867bfdb76b219",	// email:	https://tempail.com/u/15/jeltacarzi-ddf5c6eb0f/
			"7b856dedb54e159aeea08aad23f42265",	// email:	https://tempail.com/u/15/jupsaverko-9a026ceb1c/
			"eb782fa7bc22b33e1258e139d6e05ce8",	// email:	https://tempail.com/u/15/doltematri-3ac489e887/
			"2a4e4bfd05a49941a6dcb63a50c3e38f",	// email:	https://tempail.com/u/15/jepsukorte-bd4aa6e7ce/
			"cbe65f3aa68356b26e1954ae6c86c953",	// email:	https://tempail.com/u/15/nerzejupso-d435dcff9b/
			"4872d1fb598c6c050309337bb9df5b8a", // email:	https://tempail.com/u/15/borzadomla-442f870750/
			"e2488e8fbbbb3d54d6ad81a7305246c6",	// email:	https://tempail.com/u/15/lupsaremlu-e400fb29de/
			"b8776c200e6b46a4399f211dc700fad5",	// email:	https://tempail.com/u/15/folmakorzi-1a729ec678/
			"f7093a91a675633f6b1cc16c1cc4daaa",	// email:	https://tempail.com/u/15/jotrucagno-fac906b376/
			"a34cc37ee3cef8e26b878ebbf5c9ba87"	// email:	https://tempail.com/u/15/zekkujafye-da40e99b44/
		};

        // METHODS //

        // MarketStack
        [Route("marketstack")]
        [HttpGet]
        //public async Task<MarketStackObject> GetMarketStack(string _ticker)		// TODO: change?
        public async Task<MarketStackObject> getMarketStackInfo(string _ticker)
        {
            HttpClient _HttpClient = new HttpClient();
            _HttpClient.BaseAddress = new Uri("http://api.marketstack.com/v1/");
            // EXAMPLE:	http://api.marketstack.com/v1/eod?access_key=208302dbe2d07c780ba4de2dc30c56ba&symbols=DIS&limit=1


            // NOTE: Uncomment if the subfunction does NOT pass!

            // call first API
            string url = $"eod?access_key={MarketStackKeys[1]}={_ticker}&limit=1";
            var connection = await _HttpClient.GetAsync(url);
            MarketStackObject _MarketStackObject = await connection.Content.ReadAsAsync<MarketStackObject>();

            // NOTE: Subfunction has NOT been tested!

            /*
            // check if current object is valid
            if (_MarketStackObject != null)
            {
                MarketStackKeys_index = 1;
                return _MarketStackObject;  // NOTE: I assume it either has the correct data or the API key has temporarily expired.
            }

            // call API list, starting with the last known good API key (default index: 0)
            bool secondLoop = false;
            for (int i = MarketStackKeys_index; i < MarketStackKeys.Count; i++)
            {
                MarketStackKeys_index = i;
                url = $"eod?access_key={MarketStackKeys[i]}={_ticker}&limit=1";
                connection = await _HttpClient.GetAsync(url);
                _MarketStackObject = await connection.Content.ReadAsAsync<MarketStackObject>();

                // loop again if started from index zero (example: loop started at end of list).
                if (secondLoop == false && i == (MarketStackKeys.Count - 1))
                {
                    secondLoop = true;  // rerun loop if you meet 
                }

                // check if current object is valid, and function now.
                if (_MarketStackObject != null)
                {
                    return _MarketStackObject;
                }

                // STOP loop from looping more than once (example: loop started after index zero).
                if (secondLoop && MarketStackKeys_index == i && _MarketStackObject == null)
                {
                    break;
                }
            }
            */

            return _MarketStackObject;  // NOTE: check if any or all API keys are NOT valid.			
        }
        // ==================================================================================================== //
    }
}
