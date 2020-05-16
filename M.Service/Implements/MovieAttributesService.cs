using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///MovieAttributes
	///</summary>
    public class MovieAttributesService : BaseService<MovieAttributes>, IMovieAttributesService
    {
        private readonly Repository.Interfaces.IMovieAttributesRepository _repository;
        public MovieAttributesService(ILogger<MovieAttributesService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<MovieAttributes> repository) : base(logger, cache)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieAttributesRepository;
        }
    }
}
