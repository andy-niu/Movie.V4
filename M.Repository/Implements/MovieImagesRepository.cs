using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieImages
    ///</summary>
    public class MovieImagesRepository : BaseRepository<Entity.MovieImages> ,Interfaces.IMovieImagesRepository
    {
        private readonly ILogger _logger;
        public MovieImagesRepository(ILogger<MovieImagesRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
