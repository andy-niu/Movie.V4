using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///MovieBase
	///</summary>
    public class MovieBaseService : BaseService<MovieBase>, IMovieBaseService
    {
        private Repository.Interfaces.IMovieBaseRepository _repository;
        public MovieBaseService(Repository.Interfaces.IBaseRepository<MovieBase> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieBaseRepository;
        }
    }
}
