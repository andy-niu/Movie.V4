using M.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implement
{
    ///<summary>
    ///SystemConfigMenu
    ///</summary>
    public class SystemConfigMenuRepository : BaseRepository<Entity.SystemConfigMenu> ,Interface.ISystemConfigMenuRepository
    {
        private readonly ILogger _logger;
        public SystemConfigMenuRepository(ILogger<SystemConfigMenuRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
