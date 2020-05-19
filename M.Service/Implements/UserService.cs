using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///User
	///</summary>
    public class UserService : BaseService<User>, IUserService
    {
        private readonly Repository.Interfaces.IUserRepository _repository;
        public UserService(ILogger<UserService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<User> repository) : base(cache)
        {
            base._baseRepository = repository;
            base._logger = logger;
            _repository = repository as Repository.Interfaces.IUserRepository;
        }
    }
}
