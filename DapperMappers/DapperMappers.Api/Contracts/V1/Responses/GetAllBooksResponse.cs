using DapperMappers.Api.Contracts.V1.Resources;
using System.Collections.Generic;
using DapperMappers.Api.Contracts.Core;

namespace DapperMappers.Api.Contracts.V1.Responses;

public record GetAllBooksResponse(IEnumerable<BookResource> Result, int StatusCode)
    : Response<IEnumerable<BookResource>>(Result, StatusCode);