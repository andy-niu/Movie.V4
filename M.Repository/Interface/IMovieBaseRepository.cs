using System.Collections.Generic;

namespace M.Repository.Interface
{
    public interface IMovieBaseRepository : IBaseRepository
    {
        IEnumerable<M.Repository.Entity.MovieBase> GetAll(int limit = 0, int rows = 1000);
    }
}
