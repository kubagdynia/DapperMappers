using Dapper.CustomTypeHandlers.Extensions;
using DapperMappers.Domain.Models;
using DapperMappers.Domain.Repositories;
using DapperMappers.Domain.Repositories.CommandQueries;
using DbConnectionExtensions.DbConnection;
using DbConnectionExtensions.DbConnection.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DapperMappers.Domain.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterCustomTypeHandlers(this IServiceCollection services)
    {
        // Register custom type handlers
        services.RegisterDapperCustomTypeHandlers(typeof(Book).Assembly);
        return services;
    }
        
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services
            .AddSingleton<ICommandQuery, CommandQuery>()

            //.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            .AddTransient<IDbConnectionFactory>(x =>
                ActivatorUtilities.CreateInstance<DbConnectionFactory>(x, "DefaultConnection"))

            .AddScoped<IBookRepository, BookRepository>();
        //.AddScoped<IBookRepository>(x =>
        //     ActivatorUtilities.CreateInstance<BookRepository>(x, "DefaultConnection"));

        return services;
    }
}