namespace BackEnd.Models
{
    public class WSBAPIcall
    {
        // PROPERTIES //
        private static HttpClient _realClientWSB = null;

        // METHODS //
        public static HttpClient MyWSBHttp
        {
            get
            {
                if (_realClientWSB == null)
                {
                    _realClientWSB = new HttpClient();
                    _realClientWSB.BaseAddress = new Uri("https://dashboard.nbshare.io/");  // WallStreetBets API URL
                }
                return _realClientWSB;
            }
        }

        // function returns API result of a given Ticker
        public static async Task<WSBObject> GetWSBObject(string ticker)
        {
            var connection = await MyWSBHttp.GetAsync("/api/v1/apps/reddit");
            List<WSBObject> WSBObjects = await connection.Content.ReadAsAsync<List<WSBObject>>();

            WSBObject myWSBObject = new WSBObject();

            for (int i = 0; i < WSBObjects.Count; i++)
            {
                if (WSBObjects[i].ticker.ToLower() == ticker.ToLower())
                {
                    myWSBObject = WSBObjects[i];
                }
            }
            return myWSBObject;
        }
    }

    public class WSBObject
    {
        // PROPERTIES //
        public int no_of_comments { get; set; }
        public string sentiment { get; set; }
        public decimal sentiment_score { get; set; }
        public string ticker { get; set; }
    }

}
