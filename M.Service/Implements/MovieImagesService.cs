using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///MovieImages
	///</summary>
    public class MovieImagesService : BaseService<MovieImages>, IMovieImagesService
    {
        private readonly Repository.Interfaces.IMovieImagesRepository _repository;
        public MovieImagesService(ILogger<MovieImagesService> logger, Microsoft.Extensions.Caching.Distributed.IDistributedCache cache, Repository.Interfaces.IMovieImagesRepository repository) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<MovieImages>;
            base._logger = logger;
            _repository = repository;
        }
    }
}
