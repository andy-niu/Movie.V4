using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///UserRole
    ///</summary>
    public class UserRoleRepository : BaseRepository<Entity.UserRole> ,Interfaces.IUserRoleRepository
    {
        private readonly ILogger _logger;
        public UserRoleRepository(ILogger<UserRoleRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
