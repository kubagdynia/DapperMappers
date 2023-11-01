using Dapper.CustomTypeHandlers.Extensions;
using DapperMappers.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DapperMappers.Domain.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.RegisterDapperCustomTypeHandlers(typeof(Book).Assembly);

            return services;
        }
    }
}
