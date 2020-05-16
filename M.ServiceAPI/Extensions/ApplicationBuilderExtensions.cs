using M.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace M.ServiceAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        /// <summary>
        /// return ApiResult { code = 400 };
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseExceptionHandler(config =>
            {
                config.Run(async ctx =>
                {
                    var result = new ApiResult { code = ApiResultCode.SystemError, msgcn = "系统异常" };

                    var exceptionHandlerPathFeature = ctx.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error != null)
                    {
                        var error = exceptionHandlerPathFeature.Error;
                        result.msg = "system error";// error.Message; // 不应该抛出详细错误给用户

                        // logger error message.
                        var logger = LogManager.GetCurrentClassLogger();
                        logger?.Error(error, ctx.Request.Path.Value);
                    }

                    ctx.Response.ContentType = "application/json";
                    ctx.Response.StatusCode = 200;
                    await ctx.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                });
            });

            return builder;
        }

        public static IApplicationBuilder UseVersionInfo<T>(this IApplicationBuilder builder, string path = "/.version")
        {
            builder.Map(path, app =>
            {
                app.Use(async (context, next) =>
                {
                    var attr = typeof(T).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                    var infos = attr?.InformationalVersion.Split("+");
                    if (infos?.Length > 2)
                    {
                        var versionObj = infos
                            .Where(p => p.Contains(':', StringComparison.Ordinal))
                            .SkipLast(1)
                            .Select(item =>
                            {
                                var pair = item.Split(':');
                                return KeyValuePair.Create(pair[0].ToLower(CultureInfo.CurrentCulture), pair[1]);
                            })
                            .ToDictionary(k => k.Key, v => v.Value);
                        var assemblyVersion = typeof(T).Assembly.GetCustomAttribute<AssemblyVersionAttribute>();
                        versionObj["version"] = assemblyVersion == null ? "" : assemblyVersion.Version;
                        if (versionObj.ContainsKey("branch"))
                            versionObj["inside"] = "true";
                        context.Response.ContentType = "text/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(versionObj)).ConfigureAwait(false);
                    }
                });
            });
            return builder;
        }
    }
}
