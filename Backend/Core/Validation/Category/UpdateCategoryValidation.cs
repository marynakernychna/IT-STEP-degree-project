using Core.DTO.Category;
using FluentValidation;

namespace Core.Validation.Category
{
    public class UpdateCategoryValidation : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryValidation()
        {
            RuleFor(c => c.CurrentTitle)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");

            RuleFor(c => c.NewTitle)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");
        }
    }
}
