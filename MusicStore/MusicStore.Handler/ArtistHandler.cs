using AutoMapper;
using MusicStore.IHandler;
using MusicStore.IRepository;
using MusicStore.Model;
using MusicStore.MusicBrainzAPI.IService;
using System.Collections.Generic;

namespace MusicStore.Handler
{
    public class ArtistHandler : IArtistHandler
    {
        private IArtistRepository _IArtistRepository;
        private IRestClientService _IRestClientService;

        public ArtistHandler(IArtistRepository artistRepository, IRestClientService iRestClientService)
        {
            _IArtistRepository = artistRepository;
            _IRestClientService = iRestClientService;
            BootStrapper.InitializeMaps();
        }

        public SearchResultArtistsModel Search(string searchTerm,int pageNumber,int pageSize)
        {
            var turple = _IArtistRepository.PageList(c => c.ArtistName.Contains(searchTerm),pageSize: pageSize, pageNumber: pageNumber);
            return new SearchResultArtistsModel()
            {
                results = Mapper.Map<IEnumerable<ArtistModel>>(turple.Item1),
                numberOfPages = turple.Item3,
                pageSize = turple.Item4,
                page = turple.Item5,
                numberOfSearchResults = turple.Item2
            };
        }

        public ArtistReleasesModel Releases(string id)
        {
            var model = Mapper.Map<ArtistReleasesModel>(_IRestClientService.GetArtistReleases(id));
            RemoveCurrentArtistFromList(model,id);
            return model;
        }

        public ArtistReleasesModel ReturnFirstTenAlbums(string id)
        {
            var model = Mapper.Map<ArtistReleasesModel>(_IRestClientService.GetArtistAlbums(0,10,id));
            RemoveCurrentArtistFromList(model, id);
            return model;
        }

        #region Private Methods

        private void RemoveCurrentArtistFromList(ArtistReleasesModel model,string id)
        {
            foreach (var item in model.releases)
            {
                item.otherArtists.Remove(item.otherArtists.Find(c => c.id.Equals(id)));
            }
        }

        #endregion
    }
}
