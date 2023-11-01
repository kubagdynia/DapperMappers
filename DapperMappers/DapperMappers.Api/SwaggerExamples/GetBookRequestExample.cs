using DapperMappers.Api.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace DapperMappers.Api.SwaggerExamples
{
    public class GetBookRequestExample : IExamplesProvider<GetBookRequest>
    {
        public GetBookRequest GetExamples() => new() { Id = Guid.NewGuid() };
    }
}
