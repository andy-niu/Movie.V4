using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
    ///<summary>
    ///MovieAttributes
    ///</summary>
    public class MovieAttributesService : BaseService<MovieAttributes>, IMovieAttributesService
    {
        private readonly Repository.Interfaces.IMovieAttributesRepository _repository;
        public MovieAttributesService(ILogger<MovieAttributesService> logger,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache cache,
            Repository.Interfaces.IMovieAttributesRepository repository)
            : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<MovieAttributes>;
            base._logger = logger;
            _repository = repository;
        }
    }
}
