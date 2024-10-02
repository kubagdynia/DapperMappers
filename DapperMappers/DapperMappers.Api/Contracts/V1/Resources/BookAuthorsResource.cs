using System.Collections.Generic;

namespace DapperMappers.Api.Contracts.V1.Resources;

public record BookAuthorsResource
{
    public List<AuthorResource> Authors { get; set; }
}

public record AuthorResource
{
    public string Name { get; set; }

    public string Description { get; set; }
}