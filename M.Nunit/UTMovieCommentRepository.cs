using M.Common;
using M.Repository.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace M.Nunit
{
    class UTMovieCommentRepositoryDome
    {
        private TestFixture _fixture;
        private ILogger<MovieCommentRepository> _logger;
        private MovieCommentRepository _dbContext;


        public enum Company
        {
            Unknown = 0,

            [EnumValue("1")]
            Afc = 1,

            [EnumValue("2")]
            Lc = 2
        }


        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<MovieCommentRepository>>();
            this._dbContext = new MovieCommentRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }

        [Test]
        public void Add()
        {
            for (int i = 10; i < 20; i++)
            {
                var result = _dbContext.Add(new Repository.Entity.MovieComment()
                {
                    Content = $"test--{i}",
                    MovieId = 3997,
                    Parentid = 0,
                    PcitureUrl = "",
                    UserId = 1,
                    UserName = "Andy---" + i,
                    UserIp = "127.0.0.1"
                });
                Assert.IsTrue(result.Result);
               
            }
        }

        [Test]
        public void Update()
        {
            //Expression<Func<Repository.Entity.MovieComment, bool>> func = (model) => true;
            var models = _dbContext.GetEntity((model) => true).Result;

            Assert.IsNotNull(models);
            if (models != null)
            {
                models.Content = "this is test999";
                var result = _dbContext.Update(models);
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Destory()
        {
            var models = _dbContext.GetEntity((model) => true);
            Assert.IsNotNull(models.Result);
            if (models.Result != null)
            {
                var result = _dbContext.Delete(models.Result);
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Get()
        {
            var result = _dbContext.GetEntity((model) => true);
            Assert.IsNotNull(result.Result);
        }

        [Test]
        public void GetList()
        {

            Expression<Func<Repository.Entity.MovieComment, bool>> where = (model) => model.MovieId == 3997;
            Expression<Func<Repository.Entity.MovieComment, object>> orderBy = (model) => model.CommentId;

            Func<Repository.Entity.MovieAttributes, object> o = (i) => i.Alias;

            var result = _dbContext.GetEntitiesForPaging(1, 10, where, ((model) => model.CommentId),false);

            Assert.IsNotNull(result.Result.ToList());
        }


        [Test]
        public void Dome()
        {
            var saleCompanyId = 2;
            var conpany = (Company)saleCompanyId;

            Assert.AreEqual(Company.Lc, conpany);
        }
    }
}
