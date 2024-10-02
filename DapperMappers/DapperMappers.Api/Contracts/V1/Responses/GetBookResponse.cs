using DapperMappers.Api.Contracts.V1.Resources;
using DapperMappers.Api.Contracts.Core;

namespace DapperMappers.Api.Contracts.V1.Responses;

public record GetBookResponse(BookResource Result, int StatusCode)
    : Response<BookResource>(Result, StatusCode);