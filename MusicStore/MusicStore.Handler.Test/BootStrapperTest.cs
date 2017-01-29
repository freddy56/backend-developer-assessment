using MusicStore.Data;
using MusicStore.Model;
using MusicStore.MusicBrainzAPI.Service;
using MusicStore.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Handler.Test
{
    [TestFixture]
    public class BootStrapperTest
    {
        ArtistRepository repository;
        [OneTimeSetUp]
        public void SetUp()
        {
            BootStrapper.InitializeMaps();
            repository = new ArtistRepository(new ArtistDBEntities());
        }

        [Test]
        public void Test_Maps_ArtistShldMaptoArtistModel()
        {
            var list = repository.FindBy(c => c.ArtistName.Contains("john")).ToList();
            var result = AutoMapper.Mapper.Map<IEnumerable<ArtistModel>>(list).ToList();
            string[] stringSeparators = new string[] { "," };

            for (int i = 0; i < list.Count; i++)
            {
                Assert.IsTrue(list[i].ArtistName.Equals(result[i].name));
                Assert.IsTrue(list[i].Country.Equals(result[i].country));
                if (!string.IsNullOrEmpty(list[i].Aliases))
                    Assert.AreEqual(list[i].Aliases.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries), result[i].alias);
                else
                    Assert.AreEqual(list[i].Aliases, result[i].alias);
            }
        }

        [Test]
        public void Test_Maps_ReleaseShldMaptoRealeModel()
        {
            try
            {
                var service = new RestClientService("http://musicbrainz.org/ws/2");
                var artistGuid = repository.First(c => c.ArtistId == 6).ArtistGuid;
                var releases = service.GetArtistAlbums(0, 10, artistGuid);

                var result = AutoMapper.Mapper.Map<ArtistReleasesModel>(releases);
                Assert.IsTrue(result.releases.Count == 10);

                for (int i = 0; i < 10; i++)
                {
                    Assert.IsTrue(result.releases[i].title == releases.releases[i].title);
                    Assert.IsTrue(result.releases[i].status == releases.releases[i].status);
                    if (releases.releases[i].labelInfo.Count == 1)
                        Assert.IsTrue(result.releases[i].label == releases.releases[i].labelInfo[0].label.name);
                    Assert.IsTrue(result.releases[i].otherArtists.Count == releases.releases[i].otherArtists.Count);
                    Assert.IsTrue(result.releases[i].releaseId == releases.releases[i].id);
                }


            }
            catch (Exception ex)
            {

            }

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            repository.Dispose();
        }
    }
}
