using M.Repository.Entity;
using M.Service.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace M.Service.Implements
{
    ///<summary>
    ///MovieBase
    ///</summary>
    public class MovieBaseService : BaseService<MovieBase>, IMovieBaseService
    {
        private readonly Repository.Interfaces.IMovieBaseRepository _repository;
        private readonly Repository.Interfaces.IMovieAttributesRepository _attributesRepository;
        public MovieBaseService(ILogger<MovieBaseService> logger,
            Microsoft.Extensions.Caching.Distributed.IDistributedCache cache,
            Repository.Interfaces.IMovieBaseRepository repository,
            Repository.Interfaces.IMovieAttributesRepository attributesRepository
            )
            : base(cache)
        {
            base._baseRepository = repository as Repository.Interfaces.IBaseRepository<MovieBase>;
            base._logger = logger;
            _repository = repository;
            _attributesRepository = attributesRepository;
        }

        public override async Task<IEnumerable<MovieBase>> GetEntitiesForPaging(int page, int pageSize, Expression<Func<MovieBase, bool>> where)
        {
            var cacheData = await _cache.GetAsync("Movie_Attributes_All");
            if (cacheData == null)
            {
                var currentTimeUTC = DateTime.UtcNow.ToString();
                await _cache.SetAsync("Movie_Attributes_All", Encoding.UTF8.GetBytes(currentTimeUTC), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20)));
            }
            var data = await _attributesRepository.GetEntities(x => true);

            var result = await base.GetEntitiesForPaging(page, pageSize, where);

            foreach (var item in result)
            {
                var type = item.TypeAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                var region = item.RegionAttributes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();
                item.Types = data.Where(x => type.Contains(x.AttributesId)).ToArray();
                item.Regions = data.Where(x => region.Contains(x.AttributesId)).ToArray();
            }
            return result;
        }
    }
}
