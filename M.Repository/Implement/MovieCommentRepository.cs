using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///MovieComment
    ///</summary>
    public class MovieCommentRepository : BaseRepository<Entity.MovieComment> ,Interface.IMovieCommentRepository
    {
        private readonly ILogger _logger;
        public MovieCommentRepository(ILogger<MovieCommentRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
