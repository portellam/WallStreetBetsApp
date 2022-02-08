namespace BackEnd.Models
{
    public class WsbAPIcall
    {
        private static HttpClient _realClientWSB = null;

        public static HttpClient MyWSBHttp
        {
            get
            {
                if (_realClientWSB == null)
                {
                    _realClientWSB = new HttpClient();
                    _realClientWSB.BaseAddress = new Uri("https://dashboard.nbshare.io/");
                }
                return _realClientWSB;
            }
        }

        public static async Task<WsbObject> GetWsbObject(string ticker)
        {
            var connection = await MyWSBHttp.GetAsync("/api/v1/apps/reddit");
            List<WsbObject> wsbObjects = await connection.Content.ReadAsAsync<List<WsbObject>>();

            WsbObject myWsbObject = new WsbObject();

            for (int i = 0; i < wsbObjects.Count; i++)
            {
                if (wsbObjects[i].ticker.ToLower() == ticker.ToLower())
                {
                    myWsbObject = wsbObjects[i];
                }
            }
            return myWsbObject;
        }
    }

    public class WsbObject
    {
        public int no_of_comments { get; set; }
        public string sentiment { get; set; }
        public decimal sentiment_score { get; set; }
        public string ticker { get; set; }
    }

}
