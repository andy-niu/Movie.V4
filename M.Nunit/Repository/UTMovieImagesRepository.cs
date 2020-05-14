using M.Repository.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace M.Nunit
{
    class UTMovieImagesRepository
    {
        private TestFixture _fixture;
        private ILogger<MovieImagesRepository> _logger;
        private MovieImagesRepository _dbContext;

        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<MovieImagesRepository>>();
            this._dbContext = new MovieImagesRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }

        [Test]
        public void Add()
        {
            for (int i = 10; i < 20; i++)
            {
                var result = _dbContext.Add(new Repository.Entity.MovieImages()
                {
                    Id = 0,
                            MovieId = 0,
                            Status = 0,
                            Name = $"test--{i}",
                            VirtualPath = $"test--{i}",
                            OriginalUrl = $"test--{i}",
                                    OldId = 0,
                            Domain = $"test--{i}",
         
                });
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Update()
        {
            Expression<Func<Repository.Entity.MovieImages, bool>> func = (model) => true;
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

            Expression<Func<Repository.Entity.MovieImages, bool>> where = (model) => true;
            Expression<Func<Repository.Entity.MovieImages, object>> orderBy = (model) => model.Id;

            var result = _dbContext.GetEntitiesForPaging(1, 10, where, ((model) => model.Id),false);

            Assert.IsNotNull(result.Result.ToList());
        }
    }
} 
