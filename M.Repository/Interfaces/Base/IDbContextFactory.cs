using M.Repository.Context;

namespace M.Repository.Interfaces
{
    public interface IDbContextFactory
    {
        MovieBaseDbContext GetMovieDBContext();
    }
}
