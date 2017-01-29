using MusicStore.Data;
using MusicStore.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Common;

namespace MusicStore.Common.Test
{
    [TestFixture]
    public class ExtensionTest
    {
        ArtistRepository repository;
        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new ArtistRepository(new ArtistDBEntities());
        }


        [Test]
        public void Test_With_MatchWord_WhenListPopulated_ShldReturnListWithxactMatch()
        {
            var result = repository.FindBy(c => c.ArtistName.Contains("John"))
                .MatchWord("ArtistName", "John").ToList();
            Assert.IsTrue(result.Count() == 4);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            repository.Dispose();
        }
    }


}
