using NUnit.Framework;
using BackEnd.Contollers;
using BackEnd.Models;

namespace BackEnd.ControllerTests
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
        public void Test_DoFail_WillFail()
        {
            // arrange

            // act

            // assert
            Assert.Fail();
        }
    }
}
