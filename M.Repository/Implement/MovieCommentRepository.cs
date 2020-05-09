using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    public class MovieCommentRepository : BaseRepository, Interface.IMovieCommentRepository
    {
        private readonly ILogger _logger;
        public MovieCommentRepository(ILogger<MovieCommentRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}
