using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///User
    ///</summary>
    public class UserRepository : BaseRepository<Entity.User> ,Interfaces.IUserRepository
    {
        private readonly ILogger _logger;
        public UserRepository(ILogger<UserRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
