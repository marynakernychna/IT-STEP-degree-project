using Core.DTO.User;
using FluentValidation;

namespace Core.Validation.User
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidation()
        {
            RuleFor(password => password.CurrentPassword)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@$%^&*(){}:;<>,.?+_=|'~\\-])" +
                    "[A-Za-z0-9!@$%^&*(){}:;<>,.?+_=|'~\\-]*$")
                .WithMessage("Password must contain " +
                    "one or more uppercase and lowercase letters, one or more digits " +
                    "and special characters!");

            RuleFor(password => password.NewPassword)
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
