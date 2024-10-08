﻿using DapperMappers.Api.Contracts.Core;
using Swashbuckle.AspNetCore.Filters;

namespace DapperMappers.Api.SwaggerExamples;

public class NotFoundMessageExample : IExamplesProvider<NotFoundMessage>
{
    public NotFoundMessage GetExamples() => new("Not found");
}