namespace BackEnd.Models
{
	public class MarketStackAPIcall
	{
		// NOTE: made changes and proposed changes to variable names to another pattern.

		// PROPERTIES //
		//private static HttpClient _realClient = null;	// TODO: change?
														// NOTE: the idea is that you don't have to guess names if you had ten or more API classes.
		private static HttpClient _realMarketStackClient = null;

		// METHODS //
		//public static HttpClient _HttpClient			// TODO: change?
		public static HttpClient MyMarketStackHttp
		{
			get
			{
				// TODO: change?
				/*
                if (_realClient == null)
                {
                    _realClient = new HttpClient();
                    _realClient.BaseAddress = new Uri("http://api.marketstack.com/v1/eod");  // MarketStack API URL
                }
                return _realClientWSB;
                */
				if (_realMarketStackClient == null)
				{
					_realMarketStackClient = new HttpClient();
					_realMarketStackClient.BaseAddress = new Uri("http://api.marketstack.com/v1/eod");  // MarketStack API URL
				}
				return _realMarketStackClient;
			}
		}

		// function returns BaseInfo of a given Ticker
		//public static async Task<BaseInfo> GetMarketStackInfo(string apiKey, string _ticker)
		public static async Task<BaseInfo> GetStockInfo(string apiKey, string _ticker)
		{
			//var connection = await _HttpClient.GetAsync($"?access_key={apiKey}&symbols={_ticker}");  // TODO: change?
			var connection = await MyMarketStackHttp.GetAsync($"?access_key={apiKey}&symbols={_ticker}");
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
