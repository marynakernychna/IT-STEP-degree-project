using Core.DTO.Authentication;
using FluentValidation;

namespace Core.Validation.Authentication
{
    public class UserRegistrationValidation : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidation()
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

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("'{PropertyValue}' - is not an email address!");

            RuleFor(user => user.Password)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@$%^&*(){}:;<>,.?+_=|'~\\-])" +
                    "[A-Za-z0-9!@$%^&*(){}:;<>,.?+_=|'~\\-]*$")
                .WithMessage("Password must contain " +
                    "one or more uppercase and lowercase letters, one or more digits " +
                    "and special characters!");
        }
    }
}
