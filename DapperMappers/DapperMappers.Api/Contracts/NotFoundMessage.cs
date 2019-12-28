using Microsoft.AspNetCore.Http;

namespace DapperMappers.Api.Contracts
{
    public class NotFoundMessage : BaseResponse
    {
        public NotFoundMessage(string message)
        {
            StatusCode = StatusCodes.Status404NotFound;
            AddError(StatusCodes.Status404NotFound.ToString(), message);
        }
    }
}
