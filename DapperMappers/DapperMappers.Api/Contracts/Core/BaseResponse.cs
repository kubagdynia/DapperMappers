using System.Collections.Generic;
using System.Text.Json;
using DapperMappers.Api.Serializers;

namespace DapperMappers.Api.Contracts.Core
{
    public abstract class BaseResponse
    {
        public int StatusCode { get; set; }

        public IList<Error>? Errors { get; private set; }

        protected void AddError(string code, string message, string? details = null, string? userMessage = null)
        {
            Errors ??= new List<Error>();

            Errors.Add(new Error
            {
                Code = code,
                Message = message,
                Details = details,
                UserMessage = userMessage
            });
        }

        public override string ToString()
            => JsonSerializer.Serialize(this, BaseJsonOptions.GetJsonSerializerOptions);
    }

    public class Error
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
        public string? Details { get; set; }
        public string? UserMessage { get; set; }
    }
}
