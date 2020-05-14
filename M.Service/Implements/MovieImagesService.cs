using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///MovieImages
	///</summary>
    public class MovieImagesService : BaseService<MovieImages>, IMovieImagesService
    {
        private Repository.Interfaces.IMovieImagesRepository _repository;
        public MovieImagesService(Repository.Interfaces.IBaseRepository<MovieImages> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IMovieImagesRepository;
        }
    }
}
