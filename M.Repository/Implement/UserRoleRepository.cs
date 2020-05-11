using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///UserRole
    ///</summary>
    public class UserRoleRepository : BaseRepository<Entity.UserRole> ,Interface.IUserRoleRepository
    {
        private readonly ILogger _logger;
        public UserRoleRepository(ILogger<UserRoleRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
