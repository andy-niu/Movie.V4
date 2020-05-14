using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieBase
    ///</summary>
    public class MovieBaseRepository : BaseRepository<Entity.MovieBase> ,Interfaces.IMovieBaseRepository
    {
        private readonly ILogger _logger;
        public MovieBaseRepository(ILogger<MovieBaseRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
