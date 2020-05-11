using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///User
    ///</summary>
    public class UserRepository : BaseRepository<Entity.User> ,Interface.IUserRepository
    {
        private readonly ILogger _logger;
        public UserRepository(ILogger<UserRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
