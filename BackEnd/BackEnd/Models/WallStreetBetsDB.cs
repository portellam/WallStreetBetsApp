using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    public class WallStreetBetsDB
    {

    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
    }

    public class Favorite
    {
        public int id { get; set; }
        public string username { get; set; }
        public string ticker { get; set; }
    }

    public class Note
    {
        public int id { get; set; }
        public int favorite_id { get; set; }
        public string description { get; set; }
    }

    public class JoinResults
    {

    }

    public class WallStreetBetsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;");
        }
    }


}
