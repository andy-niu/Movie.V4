using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace M.Repository.Implements
{
    ///<summary>
    ///SystemConfigMenu
    ///</summary>
    public class SystemConfigMenuRepository : BaseRepository<Entity.SystemConfigMenu> ,Interfaces.ISystemConfigMenuRepository
    {
        private readonly ILogger _logger;
        public SystemConfigMenuRepository(ILogger<SystemConfigMenuRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }
    }
}	 
