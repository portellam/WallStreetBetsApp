using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
	// NOTE: all of our API calls will happen on the backend (not from the front end)

	public class WallStreetBetsDB
	{
		// METHODS //
		public static List<JoinResults> GetJoinResults(string _username)
		{
			List<JoinResults> results = null;
			using (WallStreetBetsContext context = new WallStreetBetsContext())
			{
				var query = from _Favorites in context.Favorites
							join _Notes in context.Notes on _Favorites.id equals _Notes.favorite_id into tempNotes
							from moreNotes in tempNotes.DefaultIfEmpty()
							where _Favorites.username == _username
							select new JoinResults()
							{
								username = _Favorites.username,
								ticker = _Favorites.ticker,
								favorite_id = moreNotes.favorite_id,
								description = moreNotes.description,
								note_id = moreNotes.id // NOTE: Josh test
							};
				results = query.ToList();
			}
			return results;
		}

		/*
        public static int GetFavoriteID(string username, string ticker)
        {
            int favID = 0;
            using (WallStreetBetsContext context = new WallStreetBetsContext())
            {
                var query = from myFavs in context.Favorites
                            where myFavs.username == username && myFavs.ticker == ticker
            }
        }
        */
	}

	public class User
	{
		// PROPERTIES //
		public int id { get; set; }
		public string username { get; set; }
		public string first_name { get; set; }
		//public string firstName { get; set; }
	}

	public class Favorite
	{
		// PROPERTIES //
		public int id { get; set; }
		public string ticker { get; set; }
		public string username { get; set; }
		//public List<int> note_ids { get ; set; }      // TODO: test
	}

	public class Note
	{
		// PROPERTIES //
		public int id { get; set; }
		public string description { get; set; }         // EXAMPLE: GME looks like a great buy!
		public int favorite_id { get; set; }
		//public DateTime lastEdit { get; set; }        //  TODO: optional
	}

	public class JoinResults
	{
		// PROPERTIES //
		// User
		public string username { get; set; }
		//public int user_id { get; set; }

		// Favorite
		public string ticker { get; set; }
		public int? favorite_id { get; set; }
		//public List<int> note_ids { get ; set; }      // TODO: test

		// Note
		public string description { get; set; }
		//public DateTime lastEdit { get; set; }        // TODO: test
		public int? note_id { get; set; }               // NOTE: THIS IS WHAT IM TESTING. I NEED TO ADD THIS IN THE JOINRESULTS QUERY.
	}

	public class MarketStackObject
	{
		// PROPERTIES //
		public List<StockInfo> data { get; set; }
	}

	public class StockInfo
	{
		// PROPERTIES //
		public double open { get; set; }
		public double high { get; set; }
		public double low { get; set; }
		public double close { get; set; }
		public double volume { get; set; }
		public DateTime date { get; set; }
		public string symbol { get; set; }
	}

	public class WallStreetBetsContext : DbContext
	{
		// PROPERTIES //
		public DbSet<User> Users { get; set; }
		public DbSet<Favorite> Favorites { get; set; }
		public DbSet<Note> Notes { get; set; }

		// METHODS //
		public WallStreetBetsContext(DbContextOptions<WallStreetBetsContext> options) : base(options) { }

		public WallStreetBetsContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;");
		}
	}
}
