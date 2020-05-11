using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///MovieImages
    ///</summary>
    public class MovieImagesRepository : BaseRepository<Entity.MovieImages> ,Interface.IMovieImagesRepository
    {
        private readonly ILogger _logger;
        public MovieImagesRepository(ILogger<MovieImagesRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
