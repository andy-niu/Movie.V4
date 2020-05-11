using M.Repository.Implement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace M.Nunit
{
    //[TestFixture]
    public class UTMovieAttrbutesRepository 
    {
        private TestFixture _fixture;
        private ILogger<MovieAttributesRepository> _logger;

        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<MovieAttributesRepository>>();
        }

        private Repository.Implement.MovieAttributesRepository GetMovieAttributesRepository()
        {
            //_fixture.GetService<MovieDbContext>().Database.EnsureDeleted();
            return new Repository.Implement.MovieAttributesRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }
       
        [Test]
        public void Get()
        {
            //var attr = new M.Repository.Entity.MovieAttributes()
            //{

            //};
            var db = this.GetMovieAttributesRepository();
            //Expression<Func<String, String, bool>> Like_Lambda = (item, search) => item.ToLower().Contains(search.ToLower());

            Expression<Func<Repository.Entity.MovieAttributes, bool>> func = (model) => model.ParentId == 0 && model.Status == 1;

            var result = db.GetEntitiesForPaging(1,10,func);

            Assert.IsNotNull(result.Result.ToList());
        }
       
    }
}
