using System;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public record IdResponse(Guid Id)
    {
        public Guid Id { get; private set; } = Id;
    }
}
