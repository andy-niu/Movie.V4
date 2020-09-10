using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace M.Service.Implements
{
	///<summary>
	///SystemConfigMenu
	///</summary>
    public class SystemConfigMenuService : BaseService<SystemConfigMenu>, ISystemConfigMenuService
    {
        private readonly Repository.Interfaces.ISystemConfigMenuRepository _repository;
        public SystemConfigMenuService(ILogger<SystemConfigMenuService> logger, Microsoft.Extensions.Caching.Distributed.IDistributedCache cache, Repository.Interfaces.ISystemConfigMenuRepository repository) : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<SystemConfigMenu>;
            base._logger = logger;
            _repository = repository ;
        }
    }
}
