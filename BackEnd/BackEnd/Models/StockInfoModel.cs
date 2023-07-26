using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Models
{
    [ExcludeFromCodeCoverage]
    public class StockInfoModel
    {
        public double ClosingPrice { get; set; }
        public DateTime DateTime { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double OpeningPrice { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
    }
}
