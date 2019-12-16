using System;
using System.Collections.Generic;
using DapperMappers.Core.TypeHandlers;

namespace DapperMappers.Core.Tests.Models
{
    public class TestJsonContentObject : IJsonObjectType
    {
        public int Siblings { get; set; }
        public string Nick { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> FavoriteDaysOfTheWeek { get; set; }
        public List<int> FavoriteNumbers { get; set; }
    }
}