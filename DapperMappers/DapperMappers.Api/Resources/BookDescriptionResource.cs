using System.Collections.Generic;

namespace DapperMappers.Api.Resources
{
    public class BookDescriptionResource
    {
        public LearnResource Learn { get; set; }

        public string About { get; set; }

        public FeaturesResource Features { get; set; }
    }

    public class LearnResource
    {
        public List<string> Points { get; set; }
    }

    public class FeaturesResource
    {
        public List<string> Points { get; set; }
    }
}
