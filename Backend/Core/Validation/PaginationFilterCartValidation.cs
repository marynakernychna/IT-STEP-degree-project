using Core.DTO.PaginationFilter;
using FluentValidation;

namespace Core.Validation
{
    public class PaginationFilterCartValidation : AbstractValidator<PaginationFilterCartDTO>
    {
        public PaginationFilterCartValidation()
        {
            RuleFor(pf => pf.PageNumber)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(pf => pf.PageSize)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(user => user.UserEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'{PropertyValue}' - is not an email address!");
        }
    }
}
