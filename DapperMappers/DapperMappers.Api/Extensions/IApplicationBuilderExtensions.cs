using Microsoft.AspNetCore.Builder;

namespace DapperMappers.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void UseCustomSwagger(this IApplicationBuilder app, string url = "/swagger/v1/swagger.json", string name = "My API V1")
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: url, name: name);
            });
        }
    }
}
