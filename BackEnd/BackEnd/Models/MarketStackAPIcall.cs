namespace BackEnd.Models
{
	public class MarketStackAPIcall
	{
		// PROPERTIES //
		private static HttpClient _realClient = null;

		// METHODS //
		public static HttpClient _HttpClient
		{
			get
			{
				if (_realClient == null)
				{
					_realClient = new HttpClient();
					_realClient.BaseAddress = new Uri("http://api.marketstack.com/v1/eod");  // MarketStack API URL
				}
				return _realClient;
			}
		}

		// function returns BaseInfo of a given Ticker
		public static async Task<BaseInfo> GetMarketStackInfo(string apiKey, string _ticker)
		{
			var connection = await _HttpClient.GetAsync($"?access_key={apiKey}&symbols={_ticker}");
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