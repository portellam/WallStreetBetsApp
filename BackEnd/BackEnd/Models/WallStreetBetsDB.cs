using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class WallStreetBetsDB
    {
        // METHODS //

        // CRUD FUNCTIONS
        // NOTE: all of our API calls will happen on the backend (not from the front end)

        // User table
        // GetUsers
        // AddUser
        // EditUser
        // DeleteUser           // call DeleteFavorites()
        // end table.

        // Favorite table
        // GetFavorites()
        // DeleteFavorites()    // call DeleteNote()
        // end table.

        // Note table
        // GetNotes()
        // AddNote()
        // EditNote()
        // DeleteNote()
        // end table.
    }

    public class User
    {
        // PROPERTIES //
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
    }

    public class Favorite
    {
        // PROPERTIES //
        public int id { get; set; }
        //public string username { get; set; }      // TODO: remove?
        public string ticker { get; set; }
        public int user_id { get; set; }            // foreign key
        public List<Note> noteList { get; set; }   // like a foreign key
    }

    public class Note
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime lastEdit { get; set; }    //  TODO: optional
        public int favorite_id { get; set; }
        public List<Favorite> favoriteList { get; set; }
    }

    public class JoinResults
    {
        // PROPERTIES //
        // User
        public int id { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        // Favorite
        public string ticker { get; set; }
        public int user_id { get; set; }
        public List<Note> Notes { get; set; }
    }

    public class WallStreetBetsContext : DbContext
    {
        // METHODS //
        public WallStreetBetsContext(DbContextOptions<WallStreetBetsContext> options) : base(options)
        {

        }

        // PROPERTIES //
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Note> Notes { get; set; }

        // METHODS //
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;");
        }
    }


}
