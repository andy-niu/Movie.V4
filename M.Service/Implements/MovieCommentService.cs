using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///MovieComment
	///</summary>
    public class MovieCommentService : BaseService<MovieComment>, IMovieCommentService
    {
        private readonly Repository.Interfaces.IMovieCommentRepository _repository;
        public MovieCommentService(ILogger<MovieCommentService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<MovieComment> repository) : base(logger, cache)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieCommentRepository;
        }
    }
}
