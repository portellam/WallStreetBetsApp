using Microsoft.EntityFrameworkCore;
using WallStreetBetsApp.Server.Controllers;

namespace WallStreetBetsApp.Server.Models
{
    public class WallStreetBetsDbContext : DbContext
    {
        private const string WallStreetBetsDatabaseName = "wall_street_bets";
        public DbSet<FavoriteModel> FavoriteDbSet { get; set; }
        public DbSet<NoteModel> NoteDbSet { get; set; }
        public DbSet<UserModel> UserDbSet { get; set; }

        public WallStreetBetsDbContext(DbContextOptions<WallStreetBetsDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public WallStreetBetsDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connection = $@"Server=.\SQLEXPRESS;Database={WallStreetBetsDatabaseName};Integrated Security=SSPI;";
            dbContextOptionsBuilder.UseSqlServer(connection);
        }
    }
}
