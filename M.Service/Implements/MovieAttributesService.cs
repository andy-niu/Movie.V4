using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///MovieAttributes
	///</summary>
    public class MovieAttributesService : BaseService<MovieAttributes>, IMovieAttributesService
    {
        private Repository.Interfaces.IMovieAttributesRepository _repository;
        public MovieAttributesService(Repository.Interfaces.IBaseRepository<MovieAttributes> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieAttributesRepository;
        }
    }
}
