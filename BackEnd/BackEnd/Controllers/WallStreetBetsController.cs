using Microsoft.AspNetCore.Mvc;
using BackEnd.Controllers;
using BackEnd.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallStreetBetsController : ControllerBase
    {
        /// <summary>
        /// List of API keys for MarketStack
        /// MarketStack permits 1000 requests/month.
        /// </summary>
        private static readonly List<string> MarketStackApiKeyList = new List<string> {
            //                                          //  date created,   email address
            "e2488e8fbbbb3d54d6ad81a7305246c6",         //  2022/02/16
            "208302dbe2d07c780ba4de2dc30c56ba",         //  2022/02/16

            "db5ba6ebfd7e3c28ab4eabf5d24ee9cf",         //  2023/07/26,     tihen26671@wiemei.com
            "960112bd8dc19a9be4f34e691fd6af43",         //  2023/07/26,     jexeres730@wiemei.com
            "175b74334b1ad20b81965387c33c9884",         //  2023/07/26,     radana2405@quipas.com
            "97ac1ff1ce31ffde42bbeb5e7760d9bd",         //  2023/07/26,     vofiyax821@weizixu.com
            "54d1e01dbeadeddd4a310777d4131379",         //  2023/07/26,     tijini4663@wiemei.com

            "dce0b45b5d53f724dd0b756ba09c0510",         //  2023/07/26,     nebicij492@wiemei.com
            "807da50746f00ddbceb1347e3f76bbf9",         //  2023/07/26,     rijit47321@weizixu.com
            "6e1e6de7627d5066883b5fec9261b6b9",         //  2023/07/26,     xovene1890@wiemei.com
            "651e5201c8502600b861df27bad1b799",         //  2023/07/26,     kafibi3617@sportrid.com
            "e1592170924a2b79eff89ed444d6015b"          //  2023/07/26,     dorofow114@weizixu.com
        };

        private static string LastKnownGoodMarketStackApiKey = MarketStackApiKeyList.First();

        /// <summary>
        /// Documentation: https://marketstack.com/documentation
        /// </summary>
        private static readonly string MarketStackApiUrl = "http://api.marketstack.com/v1/";

        /// <summary>
        /// Documentation: https://tradestie.com/apps/reddit/api/
        /// </summary>
        private static readonly string NbShareApiUrl = "https://tradestie.io/api/v1/";
        private readonly WallStreetBetsDbContext WallStreetBetsDbContext;

        public WallStreetBetsController(WallStreetBetsDbContext wallStreetBetsContext)
        {
            this.WallStreetBetsDbContext = wallStreetBetsContext;
        }

        [Route("favorites")]
        [HttpPost]
        public FavoriteModel AddFavorite(string username, string ticker)
        // public void AddFavorite(string username, string ticker)                                     // TODO: verify intended behavior, then decide to replace with this.
        {
            List<FavoriteModel> favoriteModelList = WallStreetBetsDbContext.Favorites.ToList();

            foreach (FavoriteModel favoriteModel in favoriteModelList)
            {
                if (String.Equals(username, favoriteModel.Username)
                    && String.Equals(ticker, favoriteModel.Ticker))
                {
                    return null;
                    //return;                                                                   // NOTE: Ditto line 33.
                }
            }

            FavoriteModel newFavoriteModel = new FavoriteModel()
            {
                Username = username,
                Ticker = ticker
            };

            WallStreetBetsDbContext.Favorites.Add(newFavoriteModel);
            WallStreetBetsDbContext.SaveChanges();
            return newFavoriteModel;
        }

        [Route("favorites")]
        [HttpDelete]
        public void DeleteFavorite(string username, string ticker)
        {
            foreach (FavoriteModel favoriteModel in WallStreetBetsDbContext.Favorites.ToList())
            {
                if (String.Equals(favoriteModel.Username, username)
                    && String.Equals(favoriteModel.Ticker, ticker))
                {
                    WallStreetBetsDbContext.Favorites.Remove(favoriteModel);
                    WallStreetBetsDbContext.SaveChanges();
                }
            }
        }

        [Route("favorites")]
        [HttpGet]
        public IEnumerable<FavoriteModel> GetFavoriteEnum()
        {
            return WallStreetBetsDbContext.Favorites;
        }

        [HttpPost]
        public void AddUser(string username, string firstName)
        {
            UserModel userModel = new UserModel()
            {
                FirstName = firstName,
                Username = username
            };

            WallStreetBetsDbContext.Users.Add(userModel);
            WallStreetBetsDbContext.SaveChanges();
        }

        [Route("joinresults")]
        [HttpGet]
        public IEnumerable<JoinResultsModel> GetJoinResults(string username)
        {
            return GetJoinResultsModelList(username);
        }

        protected internal virtual List<JoinResultsModel> GetJoinResultsModelList(string username)
        {
            if (username is null)
            {
                return null;
            }

            List<JoinResultsModel> joinResultsList = null;

            using (WallStreetBetsDbContext context = new WallStreetBetsDbContext())
            {
                var query = from myFavs in context.Favorites
                            join myNotes in context.Notes on myFavs.Id equals myNotes.FavoriteId into fullnotes
                            from morenotes in fullnotes.DefaultIfEmpty()
                            where myFavs.Username == username
                            select new JoinResultsModel()
                            {
                                Username = myFavs.Username,
                                Ticker = myFavs.Ticker,
                                FavoriteId = myFavs.Id,                    //favorite_id = morenotes.favorite_id,
                                Description = morenotes.Description,
                                NoteId = morenotes.Id
                            };
                joinResultsList = query.ToList();
            }

            return joinResultsList;
        }

        [Route("marketstack")]
        [HttpGet]
        public async Task<MarketStackModel> GetMarketStack(string ticker)
        {
            UpdateLastKnownGoodMarketStackApiKey();
            var connection = GetMarketStackApiRequest(LastKnownGoodMarketStackApiKey, ticker);
            HttpResponseMessage httpResponseMessage = await GetMarketStackHttpClient().GetAsync(connection);

            if (httpResponseMessage is null)
            {
                throw new NullReferenceException("MarketStack API key is not valid or is expired.");
            }

            return await httpResponseMessage.Content.ReadAsAsync<MarketStackModel>();
        }

        /// <summary>
        /// Documentation: https://marketstack.com/documentation
        /// </summary>
        protected internal virtual string GetMarketStackApiRequest(string marketStackApiKey, string ticker)
        {
            return String.Format($"eod?access_key={0}&symbols={1}&limit=1", marketStackApiKey, ticker);
        }

        protected internal virtual HttpClient GetMarketStackHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(MarketStackApiUrl)
            };
        }

        protected internal virtual async Task<bool> IsMarketStackApiKeyNotExpired(string marketStackApiKey)
        {
            var connection = String.Format($"eod?access_key={0}", marketStackApiKey);
            HttpResponseMessage httpResponseMessage = await GetMarketStackHttpClient().GetAsync(connection);
            return httpResponseMessage is not null;
        }

        protected internal virtual async void UpdateLastKnownGoodMarketStackApiKey()
        {
            bool isApiKeyValid = false;

            foreach (string marketStackApiKey in MarketStackApiKeyList)
            {
                isApiKeyValid = await IsMarketStackApiKeyNotExpired(marketStackApiKey);

                if (isApiKeyValid)
                {
                    LastKnownGoodMarketStackApiKey = marketStackApiKey;
                    break;
                }
            }

            if (!isApiKeyValid)
            {
                throw new Exception("MarketStack API keys are expired.");
            }
        }

        /// <summary>
        /// Gets the nb share.
        /// Notice I'm not bothering with making classes for the response. Ultimately all
        /// http requests are really just strings anyway. So I'm just reading the JSON as
        /// a string and passing it exactly as-is back out. The Angular app in turn will
        /// correctly read it as JSON. (Interestingly, the ASP.NET core seems to notice
        /// that the data is JSON and attaches the correct content type of application/json.)
        /// </summary>
        /// <returns>the nb share</returns>
        [Route("nbshare")]
        [HttpGet]
        public async Task<string> GetNbShare()
        {
            HttpClient httpClient = new HttpClient()
            {
                BaseAddress = new Uri(NbShareApiUrl)
            };

            HttpResponseMessage response = await httpClient.GetAsync("apps/reddit");
            return await response.Content.ReadAsStringAsync();
        }

        [Route("notes")]
        [HttpPost]
        public void AddNote(int favoriteId, string noteDescription)
        {
            foreach (FavoriteModel favoriteModel in WallStreetBetsDbContext.Favorites.ToList())
            {
                if (favoriteModel.Id == favoriteId)
                {
                    NoteModel noteModel = new NoteModel()
                    {
                        Id = favoriteModel.Id,
                        Description = noteDescription
                    };

                    WallStreetBetsDbContext.Notes.Add(noteModel);
                    WallStreetBetsDbContext.SaveChanges();
                    return;
                }
            }
        }

        [Route("notes")]
        [HttpDelete]
        public void DeleteNote(int noteId)
        {
            foreach (NoteModel noteModel in WallStreetBetsDbContext.Notes.ToList())
            {
                if (noteModel.Id == noteId)
                {
                    WallStreetBetsDbContext.Notes.Remove(noteModel);
                    WallStreetBetsDbContext.SaveChanges();
                    return;
                }
            }
        }

        [Route("notes")]
        [HttpPut]
        public void EditNote(int noteId, string description)
        {
            foreach (NoteModel noteModel in WallStreetBetsDbContext.Notes.ToList())
            {
                if (noteModel.Id == noteId)
                {
                    noteModel.Description = description;
                    WallStreetBetsDbContext.Notes.Update(noteModel);
                    WallStreetBetsDbContext.SaveChanges();
                    return;
                }
            }
        }

        [Route("notes")]
        [HttpGet]
        public IEnumerable<NoteModel> GetNoteList()
        {
            return WallStreetBetsDbContext.Notes;
        }

        [HttpGet]
        public IEnumerable<UserModel> GetUserEnum()
        {
            return WallStreetBetsDbContext.Users;
        }
    }
}
