using M.Repository.Context;
using M.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace M.Repository.Implements
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
