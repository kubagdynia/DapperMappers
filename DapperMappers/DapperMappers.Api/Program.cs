using System.Text.Json.Serialization;
using DapperMappers.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DapperMappers.Api.Extensions;
using DapperMappers.Api.Serializers;
using DapperMappers.Domain.Extensions;
using DapperMappers.Domain.Repositories.CommandQueries;
using DbConnectionExtensions.DbConnection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAutoMapper(typeof(Program))
    .AddSingleton<ICommandQuery, CommandQuery>()
    
    //.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
    .AddTransient<IDbConnectionFactory>(x =>
        ActivatorUtilities.CreateInstance<DbConnectionFactory>(x, "DefaultConnection"))
    
    .AddScoped<IBookRepository, BookRepository>()
    //.AddScoped<IBookRepository>(x =>
    //     ActivatorUtilities.CreateInstance<BookRepository>(x, "DefaultConnection"));
    
    .AddDomain()
    
    .AddSwagger<Program>(includeXmlComments: true, name: "v1", title: "Book API", version: "v1")
    
    .AddControllers().AddJsonOptions(options =>
    {
        // The default value is Include, which includes properties with null values in the serialized output.
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        // The default value is false, which means that the case of property names is preserved.
        options.JsonSerializerOptions.PropertyNamingPolicy = BaseJsonOptions. PropertyNamingPolicy;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting(); 

app.UseAuthorization();

app.UseCustomSwagger("/swagger/v1/swagger.json", "Book API V1");

app.MapControllers();

app.Run();


    
    