using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///MovieAttributes
    ///</summary>
    public class MovieAttributesRepository : BaseRepository<Entity.MovieAttributes> ,Interface.IMovieAttributesRepository
    {
        private readonly ILogger _logger;
        public MovieAttributesRepository(ILogger<MovieAttributesRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
