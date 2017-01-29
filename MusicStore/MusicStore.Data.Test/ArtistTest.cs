using NUnit.Framework;
using System.Linq;

namespace MusicStore.Data.Test
{
    [TestFixture]
    public class ArtistTest
    {
        ArtistDBEntities context;

        [OneTimeSetUp]
        public void SetUp()
        {
            context = new ArtistDBEntities();
        }

        [Test]
        public void Test_WhenThereAre15ArtistInArtistTable_ShldReturn15()
        {
            Assert.IsTrue(context.Artists.Count()==15);
        }

        [Test]
        public void Test_WhenArtistIDIS65f4f0c5ef9e490caee3909e7ae6b2ab_ShldReturnMetallica()
        {
            var result = context.Artists.SingleOrDefault(c=>c.ArtistGuid.Equals("65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab"));
            Assert.IsTrue(result.ArtistName.Equals("Metallica"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }
    }
}
