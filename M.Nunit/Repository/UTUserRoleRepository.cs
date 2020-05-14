using M.Repository.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace M.Nunit
{
    class UTUserRoleRepository
    {
        private TestFixture _fixture;
        private ILogger<UserRoleRepository> _logger;
        private UserRoleRepository _dbContext;

        [SetUp]
        public void Setup()
        {
            this._fixture = new TestFixture();
            this._logger = this._fixture.GetService<ILogger<UserRoleRepository>>();
            this._dbContext = new UserRoleRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        }

        [Test]
        public void Add()
        {
            for (int i = 10; i < 20; i++)
            {
                var result = _dbContext.Add(new Repository.Entity.UserRole()
                {
                    RoleId = Guid.NewGuid(),
                            Name = $"test--{i}",
                            Decription = $"test--{i}",
                 
                });
                Assert.IsTrue(result.Result);
            }
        }

        [Test]
        public void Update()
        {
            Expression<Func<Repository.Entity.UserRole, bool>> func = (model) => true;
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

            Expression<Func<Repository.Entity.UserRole, bool>> where = (model) => true;
            Expression<Func<Repository.Entity.UserRole, object>> orderBy = (model) => model.RoleId;

            var result = _dbContext.GetEntitiesForPaging(1, 10, where, ((model) => model.RoleId),false);

            Assert.IsNotNull(result.Result.ToList());
        }
    }
} 
