using Core.DTO.Authentication;
using FluentValidation;

namespace Core.Validation.Authentication
{
    public class UserLoginValidation : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidation()
        {
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
