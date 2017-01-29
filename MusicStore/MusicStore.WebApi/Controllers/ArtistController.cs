using MusicStore.Data;
using MusicStore.Handler;
using MusicStore.IHandler;
using MusicStore.Model;
using MusicStore.MusicBrainzAPI.Service;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace MusicStore.WebApi.Controllers
{
    public class ArtistController : ApiController
    {
        private IArtistHandler _IArtistHandler;

        public ArtistController(IArtistHandler iArtistHandler)
        {
            _IArtistHandler = iArtistHandler;
            //_IArtistHandler = new ArtistHandler(new ArtistRepository(new ArtistDBEntities()),
            //    new RestClientService("http://musicbrainz.org/ws/2"));
        }

        // GET artist/search/string
        [Route("artist/search/{term}/{pageNumber:int}/{pageSize:int}")]
        [HttpGet]
        public SearchResultArtistsModel Search(string term,int pageNumber,int pageSize)
        {
            return _IArtistHandler.Search(term, pageNumber:pageNumber, pageSize: pageSize);
        }

        // GET artist/{artistId}/releases
        [Route("artist/{artistId}/releases")]
        [HttpGet]
        public ArtistReleasesModel Releases(string artistId)
        {
            return _IArtistHandler.Releases(artistId);
        }

        // GET artist/{artistId}/album
        [Route("artist/{artistId}/albums")]
        [HttpGet]
        public ArtistReleasesModel albums(string artistId)
        {
            return _IArtistHandler.ReturnFirstTenAlbums(artistId);
        }
        
    }
}