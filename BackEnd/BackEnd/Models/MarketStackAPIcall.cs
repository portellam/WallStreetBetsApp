namespace BackEnd.Models
{
    public class MarketStackAPIcall
    {
        // NOTE: made changes and proposed changes to variable names to another pattern.

        // PROPERTIES //
        private static HttpClient _realMarketStackClient = null;

        // METHODS //
        //public static HttpClient _HttpClient  // TODO: change?
        // NOTE: the idea is that you don't have to guess names if you had ten API classes.
        public static HttpClient MyMarketStackHttp
        {
            get
            {
                if (_realMarketStackClient == null)
                {
                    _realMarketStackClient = new HttpClient();
                    _realMarketStackClient.BaseAddress = new Uri("http://api.marketstack.com/v1/eod");  // MarketStack API URL
                }
                return _realMarketStackClient;
            }
        }

        // function returns StockInfo of a given Ticker
        public static async Task<BaseInfo> GetStockInfo(string apiKey, string _Ticker)
        {
            //var connection = await _HttpClient.GetAsync($"?access_key={apiKey}&symbols={_Ticker}");  // TODO: change?
            var connection = await MyMarketStackHttp.GetAsync($"?access_key={apiKey}&symbols={_Ticker}");
            BaseInfo _BaseInfo = await connection.Content.ReadAsAsync<BaseInfo>();
            return _BaseInfo;
        }
    }

    public class BaseInfo
    {
        // PROPERTIES //
        public Pagination pagination { get; set; }
        public List<StockInfo> data { get; set; }
    }

    public class Pagination
    {
        // PROPERTIES //
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    
    public class StockInfo
    {
        // PROPERTIES //
        public string symbol { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
    }
}
