using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///MovieComment
	///</summary>
    public class MovieCommentService : BaseService<MovieComment>, IMovieCommentService
    {
        private Repository.Interfaces.IMovieCommentRepository _repository;
        public MovieCommentService(Repository.Interfaces.IBaseRepository<MovieComment> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieCommentRepository;
        }
    }
}
