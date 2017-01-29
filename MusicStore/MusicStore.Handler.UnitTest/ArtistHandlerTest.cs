using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using MusicStore.Data;
using MusicStore.IRepository;
using MusicStore.MusicBrainzAPI.IService;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MusicStore.MusicBrainzAPI.Model;

namespace MusicStore.Handler.UnitTest
{
    public class ArtistHandlerTest
    {
        Moq.Mock<IArtistRepository> mockArtistRepository_FindBy;
        Moq.Mock<IRestClientService> mockRestClientService_GetArtistReleases;
        Moq.Mock<IRestClientService> mockRestClientService_GetArtistAlbums;
        private string id;
        [OneTimeSetUp]
        public void SetUp()
        {
            id = Guid.NewGuid().ToString();

            mockArtistRepository_FindBy = new Moq.Mock<IArtistRepository>();
            mockArtistRepository_FindBy.Setup(c => c.FindBy(It.IsAny<Expression<Func<MusicStore.Data.Artist, bool>>>())).Returns(ReturnArtistList);

            mockRestClientService_GetArtistReleases = new Mock<IRestClientService>();
            mockRestClientService_GetArtistReleases.Setup(c => c.GetArtistReleases(id)).Returns(ReturnReleaseObject);

            mockRestClientService_GetArtistAlbums = new Mock<IRestClientService>();
            mockRestClientService_GetArtistAlbums.Setup(c => c.GetArtistAlbums(0,10,id)).Returns(ReturnReleaseObject);
        }

        [Test]
        public void Test_Search_WhnSearchTermIsJoh_ShldReturn1SearchResultArtistsModel()
        {
            var handler = new ArtistHandler(mockArtistRepository_FindBy.Object,
                mockRestClientService_GetArtistReleases.Object);
            var result = handler.Search("Joh");
            Assert.IsTrue(result.results.Count() == 1);
        }

        [Test]
        public void Test_Releases_Whn1ReleaseReturned_ShldReturn1ArtistReleaseModel()
        {
            var handler = new ArtistHandler(mockArtistRepository_FindBy.Object,
                mockRestClientService_GetArtistReleases.Object);
            var result = handler.Releases(id);
            Assert.IsTrue(result.releases.Count == 1);
            Assert.IsTrue(result.releases.First().otherArtists.Count == 1);
        }

        [Test]
        public void Test_ReturnFirstTenAlbums_Whn1ReleaseReturned_ShldReturn1ArtistReleaseModel()
        {
            var handler = new ArtistHandler(mockArtistRepository_FindBy.Object,
                mockRestClientService_GetArtistAlbums.Object);
            var result = handler.ReturnFirstTenAlbums(id);
            Assert.IsTrue(result.releases.Count == 1);
            Assert.IsTrue(result.releases.First().otherArtists.Count == 1);
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            mockArtistRepository_FindBy = null;
        }

        #region Private Methods

        private IEnumerable<MusicStore.Data.Artist> ReturnArtistList()
        {
            return new List<MusicStore.Data.Artist>(){ new MusicStore.Data.Artist(){ ArtistName="Freddie Mecury",
                 Aliases="Freddie Mecury",
                 ArtistGuid = Guid.NewGuid().ToString(),
                 ArtistId=1,
                 Country="UK"}};
        }

        private Releases ReturnReleaseObject()
        {
            return new Releases()
            {
                releases = new List<Release>(){ new Release(){ id = id, 
                labelInfo=new List<LabelInfo>(){ 
                        new LabelInfo(){ label = new Label(){ name="Sony"}}}, 
                status = "Official",
                title = "The Rock Opera",
                media = new List<Media>(){ new Media(){ numberOfTracks="19"}},
                otherArtists = new List<OtherArtists>(){ new OtherArtists(){ 
                          artist = new MusicStore.MusicBrainzAPI.Model.Artist(){ id=Guid.NewGuid().ToString()},
                           joinphrase="with. ",
                           name = "Arr. Mario Previti"},
                           new OtherArtists(){ 
                          artist = new MusicStore.MusicBrainzAPI.Model.Artist(){ id=id},
                           joinphrase="",
                           name = "Freddy Mercury"}}}}
            };
        }
        #endregion
    }
}
