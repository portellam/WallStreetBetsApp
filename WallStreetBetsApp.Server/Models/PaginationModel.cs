using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Models
{
    [ExcludeFromCodeCoverage]
    public class PaginationModel
    {
        public int Count { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
    }
}