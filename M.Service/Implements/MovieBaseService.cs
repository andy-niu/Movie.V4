using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///MovieBase
	///</summary>
    public class MovieBaseService : BaseService<MovieBase>, IMovieBaseService
    {
        private readonly Repository.Interfaces.IMovieBaseRepository _repository;
        public MovieBaseService(ILogger<MovieBaseService> logger, IMemoryCache cache, Repository.Interfaces.IMovieBaseRepository repository) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<MovieBase>;
            base._logger = logger;
            _repository = repository;
        }
    }
}
