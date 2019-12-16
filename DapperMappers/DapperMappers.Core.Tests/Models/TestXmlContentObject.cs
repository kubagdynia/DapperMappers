using DapperMappers.Core.TypeHandlers;
using System;
using System.Collections.Generic;

namespace DapperMappers.Core.Tests.Models
{
    public class TestXmlContentObject : IXmlObjectType
    {
        public int Siblings { get; set; }
        public string Nick { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> FavoriteDaysOfTheWeek { get; set; }
        public List<int> FavoriteNumbers { get; set; }
    }
}
