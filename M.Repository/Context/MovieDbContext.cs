using Microsoft.EntityFrameworkCore;

namespace M.Repository.Context
{
    public class MovieDbContext : MovieBaseDbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            
        }
    }
}
