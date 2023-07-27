using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Controllers
{
    [ExcludeFromCodeCoverage]
    public class NoteModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int FavoriteId { get; set; }
    }
}
