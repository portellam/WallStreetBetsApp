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
        // CRUD
        // Favorites table
        // GetFavorites()

        // DeleteFavorites()    // call DeleteNote()

        // Notes table
        // PostNote()

        // PutNote()

        // DeleteNote()

        // all of our API calls will happen on the backend (not from the front end)

        public static List<JoinResults> GetJoinResults(string username)
        {
            List<JoinResults> results = null;
            using (WallStreetBetsContext context = new WallStreetBetsContext())
            {
                var query = from myFavs in context.Favorites
                            join myNotes in context.Notes on myFavs.id equals myNotes.favorite_id
                            where myFavs.username == username
                            select new JoinResults()
                            {
                                id = myFavs.id,
                                username = myFavs.username,
                                ticker = myFavs.ticker,
                                favorite_id = myNotes.favorite_id,
                                description = myNotes.description,
                            };
                results = query.ToList();
            }
            return results;
        }
    }

    public class User
    {
        // PROPERTIES //
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
    }

    public class Favorite
    {
        // PROPERTIES //
        public int id { get; set; }
        public string username { get; set; }
        public string ticker { get; set; }
    }

    public class Note
    {
        public int id { get; set; }
        public int favorite_id { get; set; }
        public string description { get; set; } // GME looks like a great buy!
    }

    public class JoinResults
    {
        public int id { get; set; } // I'm gonna sleep on this, but now that I think about it... we probably don't need id because it's not being stored in the database.
        public string username { get; set; }
        public string ticker { get; set; }
        public int favorite_id { get; set; }
        public string description { get; set; }
    }

    public class WallStreetBetsContext : DbContext
    {
        public WallStreetBetsContext()
        {
        }

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
