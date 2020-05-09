using M.Repository.Entity;
using M.Repository.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace M.Repository.Implement
{
    public class MovieBaseRepository : BaseRepository,Interface.IMovieBaseRepository
    {
        private readonly ILogger _logger;
        public MovieBaseRepository(ILogger<MovieBaseRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }

        public IEnumerable<MovieBase> GetAll(int limit = 1, int rows = 20)
        {
            var db = this.GetMovieDbContext();
            var list = db.MovieBase.ToList().Skip(rows).Take(limit);
            return list;
        }
    }
}
