using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DapperMappers.Api.Extensions;
using DapperMappers.Api.Serializers;
using DapperMappers.Domain.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
    
    // register custom type handlers for Dapper (RegisterDapperCustomTypeHandlers)
    .RegisterCustomTypeHandlers()
    
    // Add domain services to the container
    .AddDomain()
    
    .AddSwaggerGenAndSwaggerExamples<Program>(includeXmlComments: true, name: "v1", title: "Book API", version: "v1")
    
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
    
    app.UseSwagger();
    app.UseSwaggerUI(opt => opt.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Book API V1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


    
    