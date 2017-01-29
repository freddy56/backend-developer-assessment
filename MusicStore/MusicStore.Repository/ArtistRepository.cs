using MusicStore.Data;
using MusicStore.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace MusicStore.Repository
{
    public class ArtistRepository: Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(DbContext context) : base(context)
        {
            
        }

        /// <summary>
        /// Return Turple<IEnumerable<T>,numberOfSearchResults, numberOfPages, pageSize, PageNumber
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<Artist>, int, int, int, int> PageList(Expression<Func<Artist, bool>> predicate, int pageSize, int pageNumber)
        {
            int numberOfSearchResults = _dbset.Where(predicate).Count();
            int numberOfPages = (int)Math.Round((double)numberOfSearchResults / (double)pageSize, MidpointRounding.AwayFromZero);
            IEnumerable<Artist> query = _dbset.Where(predicate).OrderBy(c => c.ArtistId).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable();

            return Tuple.Create(query,
                                numberOfSearchResults,
                                numberOfPages,
                                pageSize,
                                pageNumber);
        }
    }
}
