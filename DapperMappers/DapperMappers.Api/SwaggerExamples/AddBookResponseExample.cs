using DapperMappers.Api.Contracts.V1.Responses;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace DapperMappers.Api.SwaggerExamples
{
    public class AddBookResponseExample : IExamplesProvider<AddBookResponse>
    {
        public AddBookResponse GetExamples()
        {
            return new AddBookResponse(new IdResponse(Guid.NewGuid()), StatusCodes.Status201Created);
        }
    }
}
