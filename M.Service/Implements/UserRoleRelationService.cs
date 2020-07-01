using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///UserRoleRelation
	///</summary>
    public class UserRoleRelationService : BaseService<UserRoleRelation>, IUserRoleRelationService
    {
        private readonly Repository.Interfaces.IUserRoleRelationRepository _repository;
        public UserRoleRelationService(ILogger<UserRoleRelationService> logger, IMemoryCache cache, Repository.Interfaces.IUserRoleRelationRepository repository) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<UserRoleRelation>; 
            base._logger = logger;
            _repository = repository;
        }
    }
}
