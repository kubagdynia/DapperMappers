using System.Collections.Generic;

namespace DapperMappers.Api.Resources
{
    public class BookAuthorsResource
    {
        public List<AuthorResource> Authors { get; set; }
    }

    public class AuthorResource
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
