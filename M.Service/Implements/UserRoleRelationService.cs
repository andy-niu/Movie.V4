using M.Repository.Entity;
using M.Service.Interfaces;

namespace M.Service.Implements
{
	///<summary>
	///UserRoleRelation
	///</summary>
    public class UserRoleRelationService : BaseService<UserRoleRelation>, IUserRoleRelationService
    {
        private Repository.Interfaces.IUserRoleRelationRepository _repository;
        public UserRoleRelationService(Repository.Interfaces.IBaseRepository<UserRoleRelation> repository)
        {
            base._baseRepository = repository;
            _repository = repository as Repository.Interfaces.IUserRoleRelationRepository;
        }
    }
}
