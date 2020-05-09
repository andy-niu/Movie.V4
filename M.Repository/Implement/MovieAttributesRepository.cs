using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    public class MovieAttributesRepository : BaseRepository, Interface.IMovieAttributesRepository
    {
        private readonly ILogger _logger;
        public MovieAttributesRepository(ILogger<MovieAttributesRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}
