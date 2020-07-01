using M.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public override async Task<Entity.User> GetEntity(Expression<Func<Entity.User, bool>> where)
        {
            try {
                var _db =  GetMovieDbContext();
                var result = _db.User.Where(where).Include(x => x.UserRoleRelation).ThenInclude(x => x.Role).FirstOrDefaultAsync();
                //var result = await _db.Set<Entity.User>().Where(where)
                ////.SelectMany(x => x.UserRoleRelation)
                ////.Select(x => x.User)
                //.FirstOrDefaultAsync();
                //_db.Entry(result).Collection(x => x.UserRoleRelation).Load();
                return await result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                return null;
            }
        }
    }
}	 
