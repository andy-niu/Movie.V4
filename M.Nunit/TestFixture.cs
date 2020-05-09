using AutoMapper;
using M.Repository.Implement;
using M.Repository.Interface;
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

            _services.AddDbContext<Repository.Context.MovieDbContext>(app => app.UseSqlServer("Data Source=.;Initial Catalog=Movie;Integrated Security=True"));
            _services.AddScoped<IMovieAttributesRepository, MovieAttributesRepository>();

            
            //_services.AddDbContext<NposMasterDBContext>(app => app.UseSqlServer("Server=.;Initial Catalog=NPOS_PROD_Master;User Id=sa;Password=123456"));
            _services.AddDistributedMemoryCache();
            

            _services.AddSingleton<IDbContextFactory, DbContextFactory>();
            _services.AddMemoryCache();
        }

        public T GetService<T>()
        {
            return this._serviceProvider.GetRequiredService<T>();
        }

        public ServiceCollection Services => _services;
    }
}