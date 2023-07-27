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
    }
}
