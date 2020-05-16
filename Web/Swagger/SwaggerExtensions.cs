using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class SwaggerExtensions
    {
        /// <summary>
        ///     Setup swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.UseApiBehavior = false;
            });
            services.AddVersionedApiExplorer(options =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";
                options.DefaultApiVersion = new ApiVersion(2, 0);
                //options.
                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // integrate xml comments
                    var xmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                        $"{PlatformServices.Default.Application.ApplicationName}.xml");
                    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
                });
            return services;
        }


        public static IApplicationBuilder UseSwaggerEndpoints(this IApplicationBuilder app)
        {
            //var iis_path = string.Empty;
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    iis_path = Environment.GetEnvironmentVariable("ASPNETCORE_APPL_PATH");
            //    if (iis_path == "/")
            //        iis_path = "";
            //}
            //var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            //return app.UseSwagger()
            //     .UseSwaggerUI(c =>
            //     {
            //         foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            //         {
            //             c.SwaggerEndpoint($"{iis_path}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            //         }
            //     });

            var apiVersionDescriptionProvider =
                app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            return app.UseSwagger(c => { c.RouteTemplate = "{documentName}/swagger.json"; })
                .UseSwaggerUI(c =>
                {
                    c.RoutePrefix = string.Empty;
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                });
        }
    }
}
