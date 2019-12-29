using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace DapperMappers.Api.Contracts
{
    public class BadRequestMessage : BaseResponse
    {
        public BadRequestMessage()
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }

        public BadRequestMessage(IList<ValidationFailure> errors) : this()
        {
            if (errors != null && errors.Any())
            {
                foreach (ValidationFailure error in errors)
                {
                    AddError(
                        code: error.ErrorCode,
                        message: "Validation failed",
                        userMessage: error.ErrorMessage,
                        details: $"Validation failed for '{error.PropertyName}' with value '{error.AttemptedValue}'");
                }
            }
        }
    }
}
