﻿using Core.DTO.Category;
using FluentValidation;

namespace Core.Validation.Category
{
    public class CategoryCreateValidation : AbstractValidator<CreateCategoryDTO>
    {
        public CategoryCreateValidation()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("'{PropertyName}' must not be empty!")
                .Length(2, 50)
                .WithMessage("'{PropertyName}' must be between 2 and 50 letters!")
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");
        }
    }
}
