using System;
using System.Collections.Generic;
using System.Linq;
using M.Repository.Implements;
using M.Repository.Interfaces;
using M.Service.Implements;
using M.Service.Interfaces;
using M.ServiceAPI.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace M.ServiceAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder ConfigControllers(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddMvcOptions(mvc =>
                mvc.Filters.AddService(typeof(EventLogAttribute))
            ).AddJsonOptions(json =>
            {
                json.JsonSerializerOptions.PropertyNamingPolicy = null;
            }).ConfigureApiBehaviorOptions(apiBehaivor =>
            {
                //set your api behavior.
            });
            return mvcBuilder;
        }

        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<StorageOptions>(configuration.GetSection(nameof(StorageOptions)));

            services.AddCache(configuration);

            //泛型引用方式
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

            //add repositories
            services.AddScoped<IMovieCommentRepository, MovieCommentRepository>();
            services.AddScoped<IMovieImagesRepository, MovieImagesRepository>();
            services.AddScoped<ISystemConfigMenuRepository, SystemConfigMenuRepository>();
            services.AddScoped<IMovieAttributesRepository, MovieAttributesRepository>();
            services.AddScoped<IMovieBaseRepository, MovieBaseRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            //add services
            services.AddScoped<IMovieCommentService, MovieCommentService>();
            services.AddScoped<IMovieImagesService, MovieImagesService>();
            services.AddScoped<ISystemConfigMenuService, SystemConfigMenuService>();
            services.AddScoped<IMovieAttributesService, MovieAttributesService>();
            services.AddScoped<IMovieBaseService, MovieBaseService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<EventLogAttribute>();

            services.AddScoped<IDbContextFactory, DbContextFactory>();


            services.AddMemoryCache();
            return services;
        }

        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configurationRoot)
        {
            return services.AddDistributedMemoryCache();

            //if (bool.TryParse(configurationRoot["RedisConfig:Enable"], out bool enable) && enable)
            //{
            //    return services.AddDistributedRedisCache(c =>
            //    {
            //        c.Configuration = configurationRoot["RedisConfig:Connection"];
            //        c.InstanceName = configurationRoot["RedisConfig:InstanceName"];
            //    });
            //}
            //else
            //{
            //    return services.AddDistributedMemoryCache();
            //}
        }
    }
}
