using DapperMappers.Api.Contracts.V1.Resources;
using System;
using DapperMappers.Api.Contracts.Core;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public class GetBookResponse(BookResource result, int statusCode) : Response<BookResource>(result, statusCode);
}
