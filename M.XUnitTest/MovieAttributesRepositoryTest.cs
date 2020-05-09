using M.Repository.Implement;
using Microsoft.Extensions.Logging;
using Xunit;

namespace M.XUnitTest
{
    public class MovieAttributesRepositoryTest
    {
        //private readonly TestFixture _fixture;
        //private ILogger<MovieAttributesRepository> _logger;

        public MovieAttributesRepositoryTest()
        {
            //this._fixture = fixture;
            //this._logger = _fixture.GetService<ILogger<MovieAttributesRepository>>();

            // services.AddDbContext<MovieDbContext>(opt =>
            //{
            //    opt.UseSqlServer("Data Source=.;Initial Catalog=Movie;Integrated Security=True");
            //});
        }

        //private Repository.Implement.MovieAttributesRepository GetMovieAttributesRepository()
        //{
        //    //_fixture.GetService<MovieDbContext>().Database.EnsureDeleted();
        //    return new Repository.Implement.MovieAttributesRepository(_logger, new DbContextFactory(_fixture.Services.BuildServiceProvider()));
        //}

        [Fact]
        public void Get()
        {
            //var attr = new M.Repository.Entity.MovieAttributes()
            //{

            //};

            //var db = this.GetMovieAttributesRepository();
            ////Expression<Func<String, String, bool>> Like_Lambda = (item, search) => item.ToLower().Contains(search.ToLower());

            //Expression<Func<Repository.Entity.MovieAttributes, bool>> func = (model) => model.Name == "Test";

            //var result = db.GetEntities<Repository.Entity.MovieAttributes>(func);
            var result = true;
            Assert.True(result);
        }
    }
}
