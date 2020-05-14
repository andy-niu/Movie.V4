using AutoMapper;
using M.Repository.Implements;
using M.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace M.Nunit
{
    public class TestFixture
    {
        private IServiceProvider _serviceProvider;
        private readonly ServiceCollection _services;

        public TestFixture()
        {
            this._services = new ServiceCollection();
            AddServices();
            this._serviceProvider = _services.BuildServiceProvider();
        }

        private void AddServices()
        {
            _services.AddLogging();

            _services.AddDistributedMemoryCache();
            _services.AddMemoryCache();

            _services.AddDbContext<Repository.Context.MovieDbContext>(app => app.UseSqlServer("Data Source=.;Initial Catalog=Movie_Repository;Integrated Security=True"));

            _services.AddSingleton<IDbContextFactory, DbContextFactory>();
            //_services.AddScoped<IMovieAttributesRepository, MovieAttributesRepository>();
        }

        public T GetService<T>()
        {
            return this._serviceProvider.GetRequiredService<T>();
        }

        public ServiceCollection Services => _services;
    }
}