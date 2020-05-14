using M.Repository.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace M.Nunit
{
    class UTMovieBaseRepository
    {
        private TestFixture _fixture;
        private ILogger<MovieBaseRepository> _logger;
        private MovieBaseRepository _dbContext;

        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<MovieBaseRepository>>();
            this._dbContext = new MovieBaseRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }

        [Test]
        public void Add()
        {
            for (int i = 10; i < 20; i++)
            {
                var result = _dbContext.Add(new Repository.Entity.MovieBase()
                {
                    MovieId = 0,
                            Title = $"test--{i}",
                            AliasTitle = $"test--{i}",
                            Score = 0,
                            Summary = $"test--{i}",
                            Actor = $"test--{i}",
                            Time = 0,
                            Resolution = $"test--{i}",
                            Type = $"test--{i}",
                            TypeAttributes = $"test--{i}",
                            Region = $"test--{i}",
                            RegionAttributes = $"test--{i}",
                            DownloadUri = $"test--{i}",
                            ThumbUri = $"test--{i}",
                            MovieUri = $"test--{i}",
                            OldId = 0,
                                    Source = $"test--{i}",
                            Views = 0,
                            Status = 0,
                            DoubanScore = 0,
         
                });
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Update()
        {
            Expression<Func<Repository.Entity.MovieBase, bool>> func = (model) => true;
            var models = _dbContext.GetEntity(func).Result;

            Assert.IsNotNull(models);
            if (models != null)
            {
                models.UpdatedAt = DateTime.Now;
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

            Expression<Func<Repository.Entity.MovieBase, bool>> where = (model) => true;
            Expression<Func<Repository.Entity.MovieBase, object>> orderBy = (model) => model.MovieId;

            var result = _dbContext.GetEntitiesForPaging(1, 10, where, ((model) => model.MovieId),false);

            Assert.IsNotNull(result.Result.ToList());
        }
    }
} 
