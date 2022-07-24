using Core.DTO;
using FluentValidation;

namespace Core.Validation.User
{
    public class UserEditProfileInfoValidation : AbstractValidator<UserEditProfileInfoDTO>
    {
        public UserEditProfileInfoValidation()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                   "must be uppercase, the others lowercase! And only in Latin letters!");

            RuleFor(user => user.Surname)
                .NotEmpty()
                .Length(2, 50)
                .Matches(@"^[A-Z][a-z]+$")
                .WithMessage("The first letter in '{PropertyName}' " +
                    "must be uppercase, the others lowercase! And only in Latin letters!");

            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .Length(10, 20)
                .Matches(@"^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s/0-9]+$")
                .WithMessage("'{PropertyValue}' - is not an phone number!");

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'{PropertyValue}' - is not an email address!");
        }
    }
}
