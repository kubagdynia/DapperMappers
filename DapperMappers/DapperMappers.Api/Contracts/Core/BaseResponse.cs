using DapperMappers.Api.Serializers;
using System.Collections.Generic;
using System.Text.Json;

namespace DapperMappers.Api.Contracts
{
    public abstract class BaseResponse
    {
        public int StatusCode { get; set; }

        public IList<Error>? Errors { get; private set; }

        public void AddError(string code, string message, string details, string userMessage)
        {
            if (Errors == null)
            {
                Errors = new List<Error>();
            }

            Errors.Add(new Error
            {
                Code = code,
                Message = message,
                Details = details,
                UserMessage = userMessage
            });
        }

        public void AddError(string code, string message)
        {
            AddError(code, message, null, null);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, BaseJsonOptions.GetJsonSerializerOptions);
        }
    }

    public class Error
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
        public string? Details { get; set; }
        public string? UserMessage { get; set; }
    }
}
