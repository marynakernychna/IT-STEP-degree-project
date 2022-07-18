using Core.DTO.Category;
using FluentValidation;

namespace Core.Validation.Category
{
    public class CategoryIdValidation : AbstractValidator<CategoryIdDTO>
    {
        public CategoryIdValidation()
        {
            RuleFor(c => c.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' must not be greater than 0!");
        }
    }
}
