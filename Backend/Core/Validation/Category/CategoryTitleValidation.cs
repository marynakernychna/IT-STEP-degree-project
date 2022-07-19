using Core.DTO.Category;
using FluentValidation;

namespace Core.Validation.Category
{
    public class CategoryTitleValidation : AbstractValidator<CategoryDTO>
    {
        public CategoryTitleValidation()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");
        }
    }
}
