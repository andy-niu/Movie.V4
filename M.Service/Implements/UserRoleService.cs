using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///UserRole
	///</summary>
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private readonly Repository.Interfaces.IUserRoleRepository _repository;
        public UserRoleService(ILogger<UserRoleService> logger, IMemoryCache cache, Repository.Interfaces.IUserRoleRepository repository) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<UserRole>;
            base._logger = logger;
            _repository = repository;
        }
    }
}
