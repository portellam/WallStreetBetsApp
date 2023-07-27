using WallStreetBetsApp.Server.Controllers;
using WallStreetBetsApp.Server.Models;

namespace WallStreetBetsApp.Server.ControllerTests
{
    [TestFixture]
    public class WallStreetBetsControllerTests
    {
        private WallStreetBetsController wallStreetBetsController;
        private WallStreetBetsDbContext wallStreetBetsDbContext;

        [SetUp]
        public void SetUp()
        {
            wallStreetBetsDbContext = new WallStreetBetsDbContext();
            wallStreetBetsController = new WallStreetBetsController(wallStreetBetsDbContext);
        }

        [Test]
        public void Test_DoFail_WillPass()
        {
            // arrange

            // act

            // assert
            Assert.Pass();
        }

        //IsMarketStackApiNotExpired_ApiKeyIsExpired_ReturnFalse        // NOTE: this depends if the actual API key is expired. TODO: mock?
        //IsMarketStackApiNotExpired_ApiKeyIsEmptyOrNull_ReturnFalse
        //IsMarketStackApiNotExpired_ApiKeyIsNotExpired_ReturnTrue

        //GetMarketStackHttpClient_BaseAddressIsApiUrl_ReturnHttpClient

        //UpdateLastKnownGoodMarketStackApiKey_AllApiKeysAreExpired_ThrowException

        //[Test]
        //public void UpdateLastKnownGoodMarketStackApiKey_ApiKeyListAreExpired_ThrowException()
        //{
        //}

        [TestCase("abcdefghijklmnopqrstuvwxyz012345", "APPL")]
        [TestCase("abcdefghijklmnopqrstuvwxyz012345", "")]
        [TestCase("abcdefghijklmnopqrstuvwxyz012345", null)]
        [TestCase("", "APPL")]
        [TestCase("", "")]
        [TestCase("", null)]
        [TestCase(null, "APPL")]
        [TestCase(null, "")]
        [TestCase(null, null)]
        public void GetMarketStackApiRequest_ReturnStringTest(string marketStackApiKey, string ticker)
        {
            // arrange
            string expected = String.Format($"eod?access_key={0}&symbols={1}&limit=1", marketStackApiKey, ticker);

            // act
            var actual = wallStreetBetsController.GetMarketStackApiRequest(marketStackApiKey, ticker);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
