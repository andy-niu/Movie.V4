using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///SystemConfigMenu
	///</summary>
    public class SystemConfigMenuService : BaseService<SystemConfigMenu>, ISystemConfigMenuService
    {
        private Repository.Interfaces.ISystemConfigMenuRepository _repository;
        public SystemConfigMenuService(Repository.Interfaces.IBaseRepository<SystemConfigMenu> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.ISystemConfigMenuRepository;
        }
    }
}
