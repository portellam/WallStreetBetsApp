using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Controllers
{
    [ExcludeFromCodeCoverage]
    public class JoinResultsModel
    {
        public string? Description { get; set; }
        public int? FavoriteId { get; set; }
        public int? NoteId { get; set; }
        public string Ticker { get; set; }
        public string Username { get; set; }
    }
}
