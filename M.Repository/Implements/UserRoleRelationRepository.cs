using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///UserRoleRelation
    ///</summary>
    public class UserRoleRelationRepository : BaseRepository<Entity.UserRoleRelation> ,Interfaces.IUserRoleRelationRepository
    {
        private readonly ILogger _logger;
        public UserRoleRelationRepository(ILogger<UserRoleRelationRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
