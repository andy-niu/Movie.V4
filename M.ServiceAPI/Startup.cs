using M.ServiceAPI.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace M.ServiceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigControllers();

            //swagger
            services.AddSwagger();

            //register service
            services.AddApplicationService(Configuration);

            //dbcontext
            ConfigDbContext(services);

            //jwtOptions
            services.Configure<Models.Options.JwtOptions>(Configuration.GetSection(nameof(Models.Options.JwtOptions)));

            // configure jwt authentication
            var jwtOptions = Configuration.GetSection(nameof(Models.Options.JwtOptions)).Get<Models.Options.JwtOptions>();
            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseApiExceptionHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseVersionInfo<Startup>();

            app.UseRouting();

            app.UseAuthorization().UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerEndpoints();

            app.CheckHealth();
        }

        private void ConfigDbContext(IServiceCollection services)
        {
            services.AddDbContext<Repository.Context.MovieDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Movie"));
            });

        }
    }
}
