using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieAttributes
    ///</summary>
    public class MovieAttributesRepository : BaseRepository<Entity.MovieAttributes> ,Interfaces.IMovieAttributesRepository
    {
        private readonly ILogger _logger;
        public MovieAttributesRepository(ILogger<MovieAttributesRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
