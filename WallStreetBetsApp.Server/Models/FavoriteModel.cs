using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    public class FavoriteModel
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Username { get; set; }
    }
}
