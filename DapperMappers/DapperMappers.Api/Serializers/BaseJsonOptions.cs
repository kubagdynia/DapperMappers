using System.Text.Json;
using System.Text.Json.Serialization;

namespace DapperMappers.Api.Serializers;

public static class BaseJsonOptions
{
    public static bool IgnoreNullValues { get; } = true;
    public static JsonNamingPolicy PropertyNamingPolicy { get; } = JsonNamingPolicy.CamelCase;

    public static JsonSerializerOptions GetJsonSerializerOptions { get; } = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = PropertyNamingPolicy,
    };
}