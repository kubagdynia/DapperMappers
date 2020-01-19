using Dapper.CustomTypeHandlers.Extensions;
using DapperMappers.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DapperMappers.Domain
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.RegisterDapperCustomTypeHandlers(new[] { typeof(Book).Assembly });

            return services;
        }
    }
}
