using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///User
	///</summary>
    public class UserService : BaseService<User>, IUserService
    {
        private Repository.Interfaces.IUserRepository _repository;
        public UserService(Repository.Interfaces.IBaseRepository<User> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IUserRepository;
        }
    }
}
