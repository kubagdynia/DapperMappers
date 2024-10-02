using Microsoft.AspNetCore.Http;

namespace DapperMappers.Api.Contracts.Core;

public record NotFoundMessage : BaseResponse
{
    public NotFoundMessage(string message)
    {
        StatusCode = StatusCodes.Status404NotFound;
        AddError(StatusCodes.Status404NotFound.ToString(), message);
    }
}