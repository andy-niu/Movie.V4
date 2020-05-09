using M.Repository.Implement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace M.Nunit
{
    class UTMovieCommentRepository
    {
        private TestFixture _fixture;
        private ILogger<MovieCommentRepository> _logger;
        private MovieCommentRepository _dbContext;

        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<MovieCommentRepository>>();
            this._dbContext = new MovieCommentRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }

        [Test]
        public void Get()
        {

            Expression<Func<Repository.Entity.MovieComment, bool>> func = (model) => model.MovieId==1;

            var result = _dbContext.GetEntitiesForPaging(1, 10, func);

            Assert.IsNotNull(result.ToList());
        }

        [Test]
        public void Add()
        {
            for (int i = 0; i < 10; i++)
            {
                var result = _dbContext.Add(new Repository.Entity.MovieComment()
                {
                    Content = "test--" + i,
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
            Expression<Func<Repository.Entity.MovieComment, bool>> func = (model) => true;
            var models = _dbContext.GetEntity(func);

            Assert.IsNotNull(models);
            if (models != null)
            {
                var result = _dbContext.Update(models);
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Destory()
        {
            Expression<Func<Repository.Entity.MovieComment, bool>> func = (model) => true;
            var models = _dbContext.GetEntity(func);
            Assert.IsNotNull(models);
            if (models != null)
            {
                var result = _dbContext.Delete(new Repository.Entity.MovieComment()
                {

                });
                Assert.IsTrue(result.Result);
            }
        }
    }
}
