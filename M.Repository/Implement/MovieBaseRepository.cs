using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///MovieBase
    ///</summary>
    public class MovieBaseRepository : BaseRepository<Entity.MovieBase> ,Interface.IMovieBaseRepository
    {
        private readonly ILogger _logger;
        public MovieBaseRepository(ILogger<MovieBaseRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
