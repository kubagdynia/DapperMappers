using System;

namespace DapperMappers.Api.Contracts.V1.Responses
{
    public class IdResponse
    {
        public Guid Id { get; private set; }

        public IdResponse(Guid id)
        {
            Id = id;
        }
    }
}
