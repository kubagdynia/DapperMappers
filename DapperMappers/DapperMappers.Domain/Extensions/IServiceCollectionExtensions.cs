using Dapper.CustomTypeHandlers.Extensions;
using DapperMappers.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DapperMappers.Domain
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.RegisterDapperCustomTypeHandlers(typeof(Book).Assembly);

            return services;
        }
    }
}
