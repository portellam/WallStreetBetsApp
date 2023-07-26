using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Controllers
{
    [ExcludeFromCodeCoverage]
    public class FavoriteModel
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Username { get; set; }
    }
}
