using BackEnd.Controllers;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class WallStreetBetsDbContext : DbContext
    {
        public DbSet<FavoriteModel> Favorites { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<UserModel> Users { get; set; }

        public WallStreetBetsDbContext(DbContextOptions<WallStreetBetsDbContext> options) : base(options)
        {
        }

        public WallStreetBetsDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Server=.\SQLEXPRESS;Database=WSBdatabase;Integrated Security=SSPI;";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}
