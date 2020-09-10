using M.Repository.Entity;
using M.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace M.Repository.Implements
{
    ///<summary>
    ///MovieBase
    ///</summary>
    public class MovieBaseRepository : BaseRepository<Entity.MovieBase> ,Interfaces.IMovieBaseRepository
    {
        private readonly ILogger _logger;
        public MovieBaseRepository(ILogger<MovieBaseRepository> logger, IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
            this._logger = logger;
        }

        public override async Task<IEnumerable<MovieBase>> GetEntitiesForPaging<TKey>(int page, int pageSize, Expression<Func<MovieBase, bool>> where, Expression<Func<MovieBase, TKey>> order, bool isAsc = true)
        {
            var result = await base.GetEntitiesForPaging(page, pageSize, where, order, isAsc);
            return result;
        }
    }

}	 
