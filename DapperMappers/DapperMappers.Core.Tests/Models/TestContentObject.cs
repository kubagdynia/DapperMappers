using DapperMappers.Core.TypeHandlers;
using System;

namespace DapperMappers.Core.Tests.Models
{
    public class TestContentObject : IXmlObjectType
    {
        public int Siblings { get; set; }
        public string Nick { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
