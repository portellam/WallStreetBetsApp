namespace BackEnd.Models
{
    public class MarketStackAPIcall
    {
        private static HttpClient _realMarketStackClient = null;

        public static HttpClient MyMarketStackHttp
        {
            get
            {
                if (_realMarketStackClient == null)
                {
                    _realMarketStackClient = new HttpClient();
                    _realMarketStackClient.BaseAddress = new Uri("http://api.marketstack.com/v1/eod");
                }
                return _realMarketStackClient;
            }
        }

        public static async Task<BaseInfo> GetStockInfo(string apiKey, string theTicker)
        {
            var connection = await MyMarketStackHttp.GetAsync($"?access_key={apiKey}&symbols={theTicker}");
            BaseInfo myBaseInfo = await connection.Content.ReadAsAsync<BaseInfo>();
            return myBaseInfo;
        }
    }

    public class BaseInfo
    {
        public Pagination pagination { get; set; }
        public List<StockInfo> data { get; set; }
    }

    public class Pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class StockInfo
    {
        public string symbol { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
    }
}
