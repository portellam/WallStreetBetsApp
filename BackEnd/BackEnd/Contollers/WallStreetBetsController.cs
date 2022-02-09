using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;
using BackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Contollers
{
	[Route("api/[controller]")]
	[ApiController]
    // function defines the local database.
	public class WallStreetBetsController : ControllerBase
	{
		// PROPERTIES //
		private readonly WallStreetBetsContext _context;

        // METHODS //
        public WallStreetBetsController(WallStreetBetsContext context)
        {
            _context = context;
        }

        // CRUD FUNCTIONS

        // User table:
        // Route: api/<WallStreetBetsController>

        // function reads the list of Users.
        [HttpGet]
		public IEnumerable<User> GetUsers()
		{
			return _context.Users;
		}

		// function creates a User.
		[HttpPost]
		public void AddUser(string username, string firstName)
		{
            List<User> userList = _context.Users.ToList();
            for (int i = 0; i < userList.Count; i++)
            {
                if (username == userList[i].username)
                {
                    return;     // If match exists, exit function now.
                }
            }
            User myUser = new User();
			myUser.username = username;
			myUser.firstName = firstName;
            _context.Users.Add(myUser);
			_context.SaveChanges();
            // EXAMPLE: https://localhost:7262/api/WallStreetBets?username=jeffcogs&first_name=jeff
		}

        // function edits User, checks for conflicting match.
        [HttpPut]
        public void EditUser(int id, string username, string firstName)
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
            myUser.firstName = firstName;
            _context.Users.Update(myUser);
            _context.SaveChanges();
        }

        // function deletes a User.
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
        // end table.

        // Favorite table:
        // Route: api/<WallStreetBetsController>/favorite/{id}  // NOTE: is this correct?

        // NOTE: PUT/EDIT not necessary for favoriteList.

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
        public void AddFavorite(int user_id, string ticker)
        {
            List<Favorite> favoriteList = _context.Favorites.ToList();
            for (int i = 0; i < favoriteList.Count; i++)
            {
                if (ticker == favoriteList[i].ticker)
                {
                    return;     // If match exists, exit function now.
                }
            }
            Favorite newFavorite = new Favorite();
            newFavorite.ticker = ticker;
            newFavorite.user_id = user_id;
            newFavorite.noteList = new List<Note>();
            _context.Favorites.Add(newFavorite);
            _context.SaveChanges();
        }

        // function deletes Favorite.
        [Route("favorites")]
        [HttpDelete("{id}")]    //  ("removeFav?{id}")
        public void DeleteFavorite(int user_id, string ticker)
        {
            List<Favorite> favoriteList = _context.Favorites.ToList();
            for (int i = 0; i < favoriteList.Count; i++)
            {
                if (user_id == favoriteList[i].user_id && ticker == favoriteList[i].ticker)
                {
                    // TODO: recursive call DeleteNote() here?
                    _context.Favorites.Remove(favoriteList[i]);
                    _context.SaveChanges();
                    return;     // If match exists, delete the Favorite, and exit function now.
                }
            }     
        }
        // end table.

        // Note table:

        // function reads list of Notes.
        [Route("notes")]
        [HttpGet]
        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes;
        }

        // function creates Note, assigns to Favorite Ticker
        [Route("notes")]
        [HttpPost]
        public void AddNote(int favorite_id, string description)
        {
            Note newNote = new Note();
            List<Favorite> favoriteList = _context.Favorites.ToList();
            for (int i = 0; i < favoriteList.Count; i++)
            {
                if (favorite_id == favoriteList[i].id)
                {
                    newNote.favorite_id = favorite_id;
                    newNote.description = description;
                    _context.Notes.Add(newNote);
                    _context.SaveChanges();
                    return;     // If match exists, add the Note, and exit function now.
                }
            }
        }

        // function edits Note.
        [Route("notes")]
        [HttpPut]
        public void EditNote(int id, string description)
        {
            List<Note> noteList = _context.Notes.ToList();
            for (int i = 0; i < noteList.Count; i++)
            {
                if (id == noteList[i].id)
                {
                    noteList[i].description = description;
                    _context.Notes.Update(noteList[i]);
                    _context.SaveChanges();
                    return;     // If match exists, edit Note, and exit function now.
                }
            }
        }

        // function deletes Note.
        [Route("notes")]
        [HttpDelete]
        public void DeleteNote(int id)
        {
            List<Note> noteList = _context.Notes.ToList();
            for (int i = 0; i < noteList.Count; i++)
            {
                if (id == noteList[i].id)
                {
                    _context.Notes.Remove(noteList[i]);
                    _context.SaveChanges();
                    return;     // If match exists, delete Note, and exit function now.
                }
            }
        }
        // end table.
    }
}
