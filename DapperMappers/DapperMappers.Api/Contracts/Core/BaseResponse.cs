using System.Collections.Generic;
using System.Text.Json;
using DapperMappers.Api.Serializers;

namespace DapperMappers.Api.Contracts.Core;

public abstract record BaseResponse
{
    public int StatusCode { get; set; }

    public IList<Error>? Errors { get; private set; }

    protected void AddError(string code, string message, string? details = null, string? userMessage = null)
    {
        Errors ??= new List<Error>();
        Errors.Add(new Error(Message: message, Code: code, Details: details, UserMessage: userMessage));
    }

    public override string ToString()
        => JsonSerializer.Serialize(this, BaseJsonOptions.GetJsonSerializerOptions);
}

public record Error(string? Message, string? Code, string? Details, string? UserMessage);