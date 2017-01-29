using MusicStore.Data;
using MusicStore.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.MusicBrainzAPI.Service.Test
{
    [TestFixture]
    public class RestClientServiceTest
    {
        ArtistRepository repository;
        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new ArtistRepository(new ArtistDBEntities());
        }

        [Test]
        public void Test_Get_ArtistReleases_WhnRealesesExist_ShlReturn25Releases()
        {
            var service = new RestClientService("http://musicbrainz.org/ws/2");
            var result = service.GetArtistReleases(repository.First(c=>c.ArtistId==1).ArtistGuid);
            Assert.IsTrue(result.releases.Count == 25);
        }

        [Test]
        public void Test_ArtistReleases_WhnOtherArtistEqual2_ShlReturn2Artists()
        {
            var service = new RestClientService("http://musicbrainz.org/ws/2");
            var result = service.GetArtistReleases(repository.First(c => c.ArtistId == 6).ArtistGuid);
            Assert.IsTrue(result.releases.Any(c=>c.otherArtists.Count == 2));
        }

        [Test]
        public void Test_GetArtistAlbums_ShldReturnFirst10Albums()
        {
            var service = new RestClientService("http://musicbrainz.org/ws/2");
            var artistGuid = repository.First(c => c.ArtistId == 6).ArtistGuid;
            var result = service.GetArtistAlbums(0, 10, artistGuid);

            
            Assert.IsTrue(result.releases.Count == 10);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            repository.Dispose();
        }

    }
}
