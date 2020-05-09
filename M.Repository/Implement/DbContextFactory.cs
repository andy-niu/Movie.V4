using M.Repository.Context;
using M.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace M.Repository.Implement
{
    public class DbContextFactory : IDbContextFactory
    {
        private IServiceProvider _serviceProvider;
        public DbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MovieBaseDbContext GetMovieDBContext()
        {
            return _serviceProvider.GetService<MovieDbContext>();
        }
    }
}
