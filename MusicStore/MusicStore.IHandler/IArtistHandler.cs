using MusicStore.Model;
using System.Collections.Generic;

namespace MusicStore.IHandler
{
    public interface IArtistHandler
    {
        SearchResultArtistsModel Search(string searchTerm, int pageNumber, int pageSize);
        ArtistReleasesModel Releases(string id);
        ArtistReleasesModel ReturnFirstTenAlbums(string id);

    }
}
