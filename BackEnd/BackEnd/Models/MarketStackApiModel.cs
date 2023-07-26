namespace BackEnd.Models
{
    public class MarketStackApiModel
    {
        private static readonly string MarketStackApiUrl = "http://api.marketstack.com/v1/eod";
        private static HttpClient MarketStackHttpClient = null;

        public static HttpClient MyMarketStackHttpContext
        {
            get
            {
                if (MarketStackHttpClient is null)
                {
                    MarketStackHttpClient = new HttpClient();
                    MarketStackHttpClient.BaseAddress = new Uri(MarketStackApiUrl);
                }

                return MarketStackHttpClient;
            }
        }

        /// <summary>
        /// Gets the stock info for a given ticker.
        /// </summary>
        /// <param name="apiKey">the API key</param>
        /// <param name="ticker">the ticker</param>
        /// <returns>the stock info</returns>
        public static async Task<BaseModel> GetStockInfo(string apiKey, string ticker)
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                string message = String.Format("API Key must be valid. Key: \"{0}\"", apiKey);

                throw new NullReferenceException(message);
            }

            if (!string.IsNullOrEmpty(ticker))
            {
                string message = String.Format("Ticker must be valid. Ticker: \"{0}\"", ticker);

                throw new NullReferenceException(message);
            }

            string key = String.Format("?access_key={0}&symbols={1}", apiKey, ticker);
            var httpResponseMessage = await MyMarketStackHttpContext.GetAsync(key);
            BaseModel baseInfo = await httpResponseMessage.Content.ReadAsAsync<BaseModel>();
            return baseInfo;
        }
    }
}
