using Core.DTO.User;
using FluentValidation;

namespace Core.Validation.User
{
    public class ConfirmationResetPasswordValidation : AbstractValidator<ConfirmationResetPasswordDTO>
    {
        public ConfirmationResetPasswordValidation()
        {
            RuleFor(user => user.Email)
               .NotEmpty()
               .EmailAddress()
               .WithMessage("'{PropertyValue}' - is not an email address!");

        }
    }
}
