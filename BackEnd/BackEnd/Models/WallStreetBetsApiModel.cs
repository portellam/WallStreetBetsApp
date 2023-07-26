namespace BackEnd.Models
{
    public class WallStreetBetsApiModel
    {
        private static readonly string WallStreetBetsApiUrl = "https://dashboard.nbshare.io/";
        private static HttpClient WallStreetBetsHttpClient = null;

        public static HttpClient ThisWallStreetBetsHttpClient
        {
            get
            {
                if (WallStreetBetsHttpClient == null)
                {
                    WallStreetBetsHttpClient = new HttpClient();
                    WallStreetBetsHttpClient.BaseAddress = new Uri(WallStreetBetsApiUrl);
                }

                return WallStreetBetsHttpClient;
            }
        }

        /// <summary>
        /// Gets the API result of a given ticker.
        /// </summary>
        /// <param name="ticker">the ticker</param>
        /// <returns>the API result</returns>
        public static async Task<WallStreetBetsModel> GetWallStreetBetsModel(string ticker)
        {
            var connection = await ThisWallStreetBetsHttpClient.GetAsync("/api/v1/apps/reddit");
            List<WallStreetBetsModel> wallStreetBetsModelList = await connection.Content.ReadAsAsync<List<WallStreetBetsModel>>();
            WallStreetBetsModel wallStreetBetsModel = new WallStreetBetsModel();

            for (int i = 0; i < wallStreetBetsModelList.Count; i++)
            {
                if (wallStreetBetsModelList[i].Ticker.ToLower() == ticker.ToLower())
                {
                    wallStreetBetsModel = wallStreetBetsModelList[i];
                }
            }

            return wallStreetBetsModel;
        }
    }
}