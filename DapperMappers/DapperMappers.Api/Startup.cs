using DapperMappers.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using DapperMappers.Api.Extensions;
using DapperMappers.Api.Serializers;
using DapperMappers.Domain;
using DbConnectionExtensions.DbConnection;

namespace DapperMappers.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = BaseJsonOptions.IgnoreNullValues;
                options.JsonSerializerOptions.PropertyNamingPolicy = BaseJsonOptions.PropertyNamingPolicy;

            }).ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<ICommandQuery, CommandQuery>();
            
            //services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
            services.AddTransient<IDbConnectionFactory>(x =>
                ActivatorUtilities.CreateInstance<DbConnectionFactory>(x, "DefaultConnection"));

            services.AddScoped<IBookRepository, BookRepository>();
            // services.AddScoped<IBookRepository>(x =>
            //     ActivatorUtilities.CreateInstance<BookRepository>(x, "DefaultConnection"));

            services.AddDomain();

            services.AddSwagger<Startup>(includeXmlComments: true, name: "v1", title: "Book API", version: "v1");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomSwagger("/swagger/v1/swagger.json", "Book API V1");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
