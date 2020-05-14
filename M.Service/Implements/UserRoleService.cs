using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///UserRole
	///</summary>
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private Repository.Interfaces.IUserRoleRepository _repository;
        public UserRoleService(Repository.Interfaces.IBaseRepository<UserRole> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IUserRoleRepository;
        }
    }
}
