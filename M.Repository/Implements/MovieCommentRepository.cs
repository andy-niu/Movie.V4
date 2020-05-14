using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieComment
    ///</summary>
    public class MovieCommentRepository : BaseRepository<Entity.MovieComment> ,Interfaces.IMovieCommentRepository
    {
        private readonly ILogger _logger;
        public MovieCommentRepository(ILogger<MovieCommentRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
