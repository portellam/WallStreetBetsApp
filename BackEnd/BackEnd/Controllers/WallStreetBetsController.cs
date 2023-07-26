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
        private static string LastKnownGoodMarketStackApiKey = String.Empty;

        private static readonly List<string> MarketStackApiKeyList = new List<string> {
            "e2488e8fbbbb3d54d6ad81a7305246c6",         // NOTE: API key created 2022/02/16
            "208302dbe2d07c780ba4de2dc30c56ba"
        };

        private static readonly string MarketStackApiUrl = "http://api.marketstack.com/v1/";
        private static readonly string NbShareApiUrl = "https://dashboard.nbshare.io/api/v1/";
        private readonly WallStreetBetsDbContext WallStreetBetsDbContext;

        public WallStreetBetsController(WallStreetBetsDbContext wallStreetBetsContext)
        {
            this.WallStreetBetsDbContext = wallStreetBetsContext;
        }

        [Route("favorites")]
        [HttpPost]
        public FavoriteModel AddFavorite(string username, string ticker)
        {
            List<FavoriteModel> favoriteModelList = WallStreetBetsDbContext.Favorites.ToList();

            foreach (FavoriteModel favoriteModel in favoriteModelList)
            {
                if (String.Equals(username, favoriteModel.Username)
                    && String.Equals(ticker, favoriteModel.Ticker))
                {
                    return null;
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
            List<FavoriteModel> Favs = WallStreetBetsDbContext.Favorites.ToList();
            for (int i = 0; i < Favs.Count; i++)
            {
                if (username == Favs[i].Username && ticker == Favs[i].Ticker)
                {
                    WallStreetBetsDbContext.Favorites.Remove(Favs[i]);
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
            UserModel myUser = new UserModel();
            myUser.Username = username;
            myUser.FirstName = firstName;
            WallStreetBetsDbContext.Users.Add(myUser);
            WallStreetBetsDbContext.SaveChanges();
        }

        [Route("joinresults")]
        [HttpGet]
        public IEnumerable<JoinResultsModel> GetJoinResults(string username)
        {
            List<JoinResultsModel> myJoinResults = GetJoinResultsModelList(username);
            return myJoinResults;
        }

        protected internal virtual List<JoinResultsModel> GetJoinResultsModelList(string username)
        {
            List<JoinResultsModel> joinResultsList = null;

            if (username is null)
            {
                return joinResultsList;
            }

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
            var connection = GetMarketStackConnection(LastKnownGoodMarketStackApiKey, ticker);
            HttpResponseMessage httpResponseMessage = await GetMarketStackHttpClient().GetAsync(connection);

            if (httpResponseMessage is null)
            {
                throw new NullReferenceException("MarketStack API Key is not valid or is expired.");
            }

            MarketStackModel marketStackObject = await httpResponseMessage.Content.ReadAsAsync<MarketStackModel>();
            return marketStackObject;
        }

        protected internal virtual string GetMarketStackConnection(string marketStackApiKey, string ticker)
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

        protected internal virtual async Task<bool> IsMarketStackAPIKeyNotExpired(string marketStackApiKey)
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
                isApiKeyValid = await IsMarketStackAPIKeyNotExpired(marketStackApiKey);

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
        /// <returns>the</returns>
        [Route("nbshare")]
        [HttpGet]
        public async Task<string> GetNbShare()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(NbShareApiUrl);
            HttpResponseMessage response = await httpClient.GetAsync("apps/reddit");
            return await response.Content.ReadAsStringAsync();
        }

        [Route("notes")]
        [HttpPost]
        public void AddNote(int favID, string noteDescription)
        {
            NoteModel myNote = new NoteModel();
            List<FavoriteModel> favoriteRecords = WallStreetBetsDbContext.Favorites.ToList();

            for (int i = 0; i < favoriteRecords.Count; i++)
            {
                if (favoriteRecords[i].Id == favID)
                {
                    myNote.FavoriteId = favID;
                    myNote.Description = noteDescription;

                    WallStreetBetsDbContext.Notes.Add(myNote);
                    WallStreetBetsDbContext.SaveChanges();
                }
            }
        }   

        [Route("notes")]
        [HttpDelete]
        public void DeleteNote(int noteId)
        {
            List<NoteModel> notesList = WallStreetBetsDbContext.Notes.ToList();
            NoteModel noteToDelete = new NoteModel();

            for (int i = 0; i < notesList.Count; i++)
            {
                if (notesList[i].Id == noteId)
                {
                    noteToDelete = notesList[i];

                    WallStreetBetsDbContext.Notes.Remove(noteToDelete);
                    WallStreetBetsDbContext.SaveChanges();
                }
            }
        }

        [Route("notes")]
        [HttpPut]
        public void EditNote(int noteID, string description)
        {
            List<NoteModel> notesList = WallStreetBetsDbContext.Notes.ToList();
            NoteModel myNote = new NoteModel();

            for (int i = 0; i < notesList.Count; i++)
            {
                if (notesList[i].Id == noteID)
                {
                    notesList[i].Description = description;
                    myNote = notesList[i];

                    WallStreetBetsDbContext.Notes.Update(myNote);
                    WallStreetBetsDbContext.SaveChanges();
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
