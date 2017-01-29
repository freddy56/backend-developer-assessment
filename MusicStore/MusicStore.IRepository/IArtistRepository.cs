using IRepository;
using MusicStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.IRepository
{
    public interface IArtistRepository : IRepository<Artist>
    {
        /// <summary>
        /// Return Turple<IEnumerable<ArtistT>,numberOfSearchResults, numberOfPages, pageSize, PageNumber
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Tuple<IEnumerable<Artist>, int, int, int, int> PageList(Expression<Func<Artist, bool>> predicate, int pageSize, int pageNumber);
    }
}
