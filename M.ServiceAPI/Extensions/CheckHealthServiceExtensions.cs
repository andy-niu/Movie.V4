using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace M.ServiceAPI.Extensions
{
    public static class CheckHealthServiceExtensions
    {
        public static IApplicationBuilder CheckHealth(this IApplicationBuilder builder)
        {
            builder.Map("/.version", branch =>
            {
                branch.Use(async (context, next) =>
                {
                    var attr = typeof(CheckHealthServiceExtensions).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                    var infos = attr?.InformationalVersion.Split("+");
                    if (infos?.Length > 2)
                    {
                        var versionObj = infos.Where(p => p.Contains(':', StringComparison.CurrentCulture)).SkipLast(1)
                            .Select(item =>
                            {
                                var pair = item.Split(':');
                                return KeyValuePair.Create(pair[0].ToLower(CultureInfo.CurrentCulture), pair[1]);
                            })
                            .ToDictionary(k => k.Key, v => v.Value);
                        var assemblyVersion = typeof(CheckHealthServiceExtensions).Assembly.GetCustomAttribute<AssemblyVersionAttribute>();
                        versionObj["version"] = assemblyVersion == null ? "" : assemblyVersion.Version;
                        if (versionObj.ContainsKey("branch"))
                            versionObj["inside"] = "true";
                        context.Response.ContentType = "text/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(versionObj)).ConfigureAwait(true);
                    }
                    else if (infos != null && infos.Any())
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(string.Join(",", infos))).ConfigureAwait(true);

                });
            });
            return builder;
        }
    }
}
