﻿using System;
using DapperMappers.Api.Contracts.Core;

namespace DapperMappers.Api.Contracts.V1.Responses;

public record AddBookResponse : Response<IdResponse>
{
    public AddBookResponse(IdResponse result, int statusCode) : base(result, statusCode)
    {
    }

    public AddBookResponse(Guid id, int statusCode) : base(new IdResponse(id), statusCode)
    {
    }
}