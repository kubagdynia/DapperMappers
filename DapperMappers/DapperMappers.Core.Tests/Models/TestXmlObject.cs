using System;

namespace DapperMappers.Core.Tests.Models
{
    public class TestXmlObject
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime StartWork { get; set; }
        public TestXmlContentObject Content { get; set; }
    }
}
