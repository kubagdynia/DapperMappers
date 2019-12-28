using DapperMappers.Api.Resources;
using System.Collections.Generic;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public class GetAllBooksResponse : Response<IEnumerable<BookResource>>
    {
        public GetAllBooksResponse(IEnumerable<BookResource> result, int statusCode) : base(result, statusCode)
        {

        }
    }
}
