using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace DapperMappers.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger<T>(this IServiceCollection services, bool includeXmlComments = false,
            string name = "v1", string title = "My API", string version = "v1")
            => AddSwagger<T>(services, typeof(T).Assembly, includeXmlComments, name, title, version);

        public static IServiceCollection AddSwagger<T>(this IServiceCollection services, Assembly assembly, bool includeXmlComments = false,
            string name = "v1", string title = "My API", string version = "v1")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: name, new OpenApiInfo { Title = title, Version = version });

                if (includeXmlComments)
                {
                    c.ExampleFilters();

                    var xmlFile = $"{assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }

            });

            if (includeXmlComments)
            {
                services.AddSwaggerExamplesFromAssemblyOf<T>();
            }

            return services;
        }
    }
}
