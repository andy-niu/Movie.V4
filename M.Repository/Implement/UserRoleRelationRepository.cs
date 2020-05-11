using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///UserRoleRelation
    ///</summary>
    public class UserRoleRelationRepository : BaseRepository<Entity.UserRoleRelation> ,Interface.IUserRoleRelationRepository
    {
        private readonly ILogger _logger;
        public UserRoleRelationRepository(ILogger<UserRoleRelationRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
