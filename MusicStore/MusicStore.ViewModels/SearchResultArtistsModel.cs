using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Model
{
    public class SearchResultArtistsModel
    {
        public IEnumerable<ArtistModel> results;
        public int numberOfSearchResults;
        public int page;
        public int pageSize;
        public int numberOfPages;
    }
}
