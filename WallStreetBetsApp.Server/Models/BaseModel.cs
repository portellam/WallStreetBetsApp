using System.Diagnostics.CodeAnalysis;

namespace WallStreetBetsApp.Server.Models
{
    [ExcludeFromCodeCoverage]
    public class BaseModel
    {
        public PaginationModel PaginationModel { get; set; }
        public List<StockInfoModel> StockInfoModelList { get; set; }
    }
}
