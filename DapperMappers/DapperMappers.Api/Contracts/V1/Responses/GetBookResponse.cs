using DapperMappers.Api.Resources;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public class GetBookResponse : Response<BookResource>
    {
        public GetBookResponse(BookResource result, int statusCode) : base(result, statusCode)
        {

        }
    }
}
