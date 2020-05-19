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
        public UserRoleRelationService(ILogger<UserRoleRelationService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<UserRoleRelation> repository) : base(cache)
        {
            base._baseRepository = repository;
            base._logger = logger;
            _repository = repository as Repository.Interfaces.IUserRoleRelationRepository;
        }
    }
}
