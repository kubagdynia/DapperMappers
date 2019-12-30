using DapperMappers.Api.Contracts.V1.Requests;
using FluentValidation;

namespace DapperMappers.Api.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(b => b.DateOfPublication)
                .NotEmpty();

            RuleFor(b => b.Isbn)
                .MaximumLength(15);

            RuleFor(b => b.ShortDescription)
                .MaximumLength(500);

            RuleFor(b => b.Publisher)
                .MaximumLength(200);

            RuleFor(b => b.Url)
                .MaximumLength(200);
        }
    }
}
