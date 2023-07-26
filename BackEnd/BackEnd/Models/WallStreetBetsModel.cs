using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Models
{
    [ExcludeFromCodeCoverage]
    public class WallStreetBetsModel
    {
        public int CommentCount { get; set; }
        public string Sentiment { get; set; }
        public decimal SentimentScore { get; set; }
        public string Ticker { get; set; }
    }
}
