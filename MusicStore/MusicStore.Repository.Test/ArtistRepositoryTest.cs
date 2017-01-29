using MusicStore.Data;
using NUnit.Framework;
using System;
using System.Linq;

namespace MusicStore.Repository.Test
{
    [TestFixture]
    public class ArtistRepositoryTest
    {
        ArtistRepository repository;
        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new ArtistRepository(new ArtistDBEntities());

            double test = (double)11 / (double)2;
            var test2 = Math.Round(test, MidpointRounding.AwayFromZero);
        }

        [Test]
        public void Test_All_ShldReturn15Objects()
        {
            var count = repository.All().Count();
            Assert.IsTrue(count == 15);
        }

        [Test]
        public void Test_FindBy_WhenArtistNameContainsJohn_ShldReturn5Objects()
        {
            var result = repository.FindBy(c => c.ArtistName.Contains("john"));
            Assert.IsTrue(result.Count() == 5);
        }

        [Test]
        public void Test_PageList_WhenArtistNameContainsJohAndPageSizeIs2AndPageNumber1_ShldReturn4Objects()
        {
            var result = repository.PageList(c => c.ArtistName.Contains("joh"), 4, 1);
            var test = result.Item1.ToList();
            Assert.IsTrue(result.Item1.Count() == 4);
        }

        [Test]
        public void Test_PageList_WhenArtistNameContainsJohAndPageSizeIs4AndPageNumber2_ShldReturn2Objects()
        {
            var result = repository.PageList(c => c.ArtistName.Contains("joh"), 4, 2);
            var test = result.Item1.ToList();
            Assert.IsTrue(result.Item1.Count() == 2);
        }

        [Test]
        public void Test_PageList_WhenArtistNameDoesNotExistAndPageSizeIs4AndPageNumber2_ShldReturn2Objects()
        {
            var result = repository.PageList(c => c.ArtistName.Contains("1234567"), 4, 2);
            var test = result.Item1.ToList();
            Assert.IsTrue(result.Item1.Count() == 2);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            repository.Dispose();
        }
    }
}
