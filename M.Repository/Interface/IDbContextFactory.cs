using M.Repository.Context;

namespace M.Repository.Interface
{
    public interface IDbContextFactory
    {
        MovieBaseDbContext GetMovieDBContext();
    }
}
