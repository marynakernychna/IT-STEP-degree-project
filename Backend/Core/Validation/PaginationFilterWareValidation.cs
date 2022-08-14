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
                .NotEmpty();
        }
    }
}
