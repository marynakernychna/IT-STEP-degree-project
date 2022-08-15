using Core.DTO.PaginationFilter;
using FluentValidation;

namespace Core.Validation
{
    internal class PaginationFilterWareValidation : AbstractValidator<PaginationFilterWareDTO>
    {
        public PaginationFilterWareValidation()
        {
            RuleFor(pf => pf.PageNumber)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(pf => pf.PageSize)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(pf => pf.CategoryTitle)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");
        }
    }
}
