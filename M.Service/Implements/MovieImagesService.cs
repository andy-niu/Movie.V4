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
        public MovieImagesService(ILogger<MovieImagesService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<MovieImages> repository) : base(cache)
        {
            base._baseRepository = repository;
            base._logger = logger;
            _repository = repository as Repository.Interfaces.IMovieImagesRepository;
        }
    }
}
