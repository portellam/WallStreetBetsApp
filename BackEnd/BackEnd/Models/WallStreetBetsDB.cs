﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class WallStreetBetsDB
    {
        // METHODS //
        // NOTE: all of our API calls will happen on the backend (not from the front end)
        public static List<JoinResults> GetJoinResults(string username)
        {
            List<JoinResults> results = null;
            using (WallStreetBetsContext context = new WallStreetBetsContext())
            {
                var query = from myFavs in context.Favorites
                            join myNotes in context.Notes on myFavs.id equals myNotes.favorite_id into fullnotes
                            from morenotes in fullnotes.DefaultIfEmpty()
                            where myFavs.username == username
                            select new JoinResults()
                            {
                                username = myFavs.username,
                                ticker = myFavs.ticker,
                                favorite_id = myFavs.id, //favorite_id = morenotes.favorite_id,
                                description = morenotes.description,
                                note_id = morenotes.id // THIS IS WHAT IM TESTING RIGHT NOW.
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
    }

    public class Favorite
    {
        // PROPERTIES //
        public int id { get; set; }
        public string ticker { get; set; }
        public string username { get; set; }
        //public int user_id { get; set; }            // foreign key
        //public List<Note> noteList { get; set; }    // like a foreign key
    }

    public class Note
    {
        public int id { get; set; }      
        public string description { get; set; } // EXAMPLE: GME looks like a great buy!
        public int favorite_id { get; set; }
        //public DateTime lastEdit { get; set; }      //  TODO: optional
        //public List<Favorite> favoriteList { get; set; }
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
        // Note
        public string description { get; set; }


        public int? note_id { get; set; } // THIS IS WHAT IM TESTING. I NEED TO ADD THIS IN THE JOINRESULTS QUERY.
    }

    public class MarketStackObject
    {
        public List<StockInfo> data { get; set; }
    }

    public class StockInfo
    {
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
        public WallStreetBetsContext(DbContextOptions<WallStreetBetsContext> options) : base(options)
        {

        }

        public WallStreetBetsContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;");
        }
    }

}
