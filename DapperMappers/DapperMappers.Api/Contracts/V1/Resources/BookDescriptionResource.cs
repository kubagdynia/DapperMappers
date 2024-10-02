using System.Collections.Generic;

namespace DapperMappers.Api.Contracts.V1.Resources;

public record BookDescriptionResource
{
    public LearnResource Learn { get; set; }

    public string About { get; set; }

    public FeaturesResource Features { get; set; }
}

public record LearnResource
{
    public List<string> Points { get; set; }
}

public record FeaturesResource
{
    public List<string> Points { get; set; }
}