using M.Repository.Entity;
using M.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace M.Repository.Implements
{
    public class RefreshTokenRepository : BaseRepository<Entity.RefreshToken>, Interfaces.IRefreshTokenRepository
    {
        private readonly ILogger _logger;
        public RefreshTokenRepository(ILogger<RefreshTokenRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }

        public async Task<bool> UpdateIsActive(RefreshToken entity)
        {
            var _db = GetMovieDbContext();
            var result = _db.RefreshToken.Where(x => x.UserId == entity.UserId);
            foreach (var item in result.ToList())
            {
                item.Revoked = entity.Revoked;
                item.RevokedByIp = entity.RevokedByIp;
            }
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
