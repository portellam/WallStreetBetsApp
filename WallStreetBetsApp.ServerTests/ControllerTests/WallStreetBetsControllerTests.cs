using Microsoft.EntityFrameworkCore;
using Moq;
using WallStreetBetsApp.Server.Controllers;
using WallStreetBetsApp.Server.Models;

namespace WallStreetBetsApp.Server.ControllerTests
{
    [TestFixture]
    public class WallStreetBetsControllerTests
    {
        private WallStreetBetsController wallStreetBetsController;
        private WallStreetBetsDbContext wallStreetBetsDbContext;
        private Mock<WallStreetBetsController> mockWallStreetBetsController;
        private Mock<WallStreetBetsDbContext> mockWallStreetBetsDbContext;
        private Mock<DbSet<NoteModel>> mockNoteDbSet;

        private static List<NoteModel> noteList = new List<NoteModel>
        {
            new NoteModel()
            {
                Id = 0,
                Description = "First note"
            },
            new NoteModel()
            {
                Id = 1,
                Description = "Second note"
            },
            new NoteModel()
            {
                Id = 2,
                Description = "Third note"
            }
        };

        [SetUp]
        public void SetUp()
        {
            wallStreetBetsController = new WallStreetBetsController(new WallStreetBetsDbContext());
            mockWallStreetBetsController = new Mock<WallStreetBetsController>(new WallStreetBetsDbContext());
        }

        private static IEnumerable<TestCaseData> NoteListTestCaseData()
        {
            foreach (NoteModel noteModel in noteList)
            {
                yield return new TestCaseData(noteModel);
            };
        }

        //IsMarketStackApiNotExpired_ApiKeyIsExpired_ReturnFalse        // NOTE: this depends if the actual API key is expired. TODO: mock?
        //IsMarketStackApiNotExpired_ApiKeyIsEmptyOrNull_ReturnFalse
        //IsMarketStackApiNotExpired_ApiKeyIsNotExpired_ReturnTrue

        //GetMarketStackHttpClient_BaseAddressIsApiUrl_ReturnHttpClient

        //UpdateLastKnownGoodMarketStackApiKey_AllApiKeysAreExpired_ThrowException

        [TestCaseSource(nameof(NoteListTestCaseData))]
        public void DeleteNote_MatchNoteId_DoDeleteNoteAndSaveChangesTest(NoteModel noteModel)              // NOTE: this test fails.
        {
            // arrange
            noteList.ForEach(noteModel => mockWallStreetBetsDbContext.Object.NoteDbSet.Add(noteModel));     // NOTE: this throws a null ref exception.
            mockWallStreetBetsDbContext.Setup(x => x.NoteDbSet.ToList()).Returns(noteList).Verifiable();
            mockWallStreetBetsDbContext.Setup(x => x.NoteDbSet.Remove(noteModel)).Verifiable();
            mockWallStreetBetsDbContext.Setup(x => x.SaveChanges()).Verifiable();

            // act
            wallStreetBetsController.DeleteNote(noteModel.Id);
            var result = mockWallStreetBetsDbContext.Object.NoteDbSet.Contains(noteModel);

            // assert
            mockWallStreetBetsDbContext.Verify(x => x.Remove(noteModel), Times.Once);
            mockWallStreetBetsDbContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreNotEqual(mockWallStreetBetsDbContext.Object.NoteDbSet, noteList);
            Assert.IsFalse(result);            
        }

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
