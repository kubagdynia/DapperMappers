using DapperMappers.Api.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace DapperMappers.Api.SwaggerExamples
{
    public class DeleteBookRequestExample : IExamplesProvider<DeleteBookRequest>
    {
        public DeleteBookRequest GetExamples() => new DeleteBookRequest { Id = Guid.NewGuid() };
    }
}
