using System.Diagnostics.CodeAnalysis;

namespace BackEnd.Models
{
    [ExcludeFromCodeCoverage]
    public class MarketStackModel
    {
        public List<StockInfoModel> StockInfoModelList { get; set; }
    }
}
