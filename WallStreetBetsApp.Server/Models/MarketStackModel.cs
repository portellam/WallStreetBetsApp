using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Models
{
    [ExcludeFromCodeCoverage]
    public class MarketStackModel
    {
        public List<StockInfoModel> StockInfoModelList { get; set; }
    }
}
