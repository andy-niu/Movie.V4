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
        public SystemConfigMenuService(ILogger<SystemConfigMenuService> logger, IMemoryCache cache, Repository.Interfaces.IBaseRepository<SystemConfigMenu> repository) : base(cache)
        {
            base._baseRepository = repository;
            base._logger = logger;
            _repository = repository as Repository.Interfaces.ISystemConfigMenuRepository;
        }
    }
}
