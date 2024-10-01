using DapperMappers.Api.Contracts.V1.Resources;
using System.Collections.Generic;
using DapperMappers.Api.Contracts.Core;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public class GetAllBooksResponse(IEnumerable<BookResource> result, int statusCode)
        : Response<IEnumerable<BookResource>>(result, statusCode);
}
