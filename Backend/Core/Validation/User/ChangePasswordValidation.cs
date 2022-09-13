using Core.DTO.User;
using FluentValidation;

namespace Core.Validation.User
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidation()
        {
            RuleFor(dto => dto.CurrentPassword)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@$%^&*(){}:;<>,.?+_=|'~\\-])" +
                    "[A-Za-z0-9!@$%^&*(){}:;<>,.?+_=|'~\\-]*$")
                .WithMessage("Password must contain " +
                    "one or more uppercase and lowercase letters, one or more digits " +
                    "and special characters!");

            RuleFor(dto => dto.NewPassword)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@$%^&*(){}:;<>,.?+_=|'~\\-])" +
                    "[A-Za-z0-9!@$%^&*(){}:;<>,.?+_=|'~\\-]*$")
                .WithMessage("Password must contain " +
                    "one or more uppercase and lowercase letters, one or more digits " +
                    "and special characters!")
                .NotEqual(dto => dto.CurrentPassword)
                .WithMessage("The passwords match!");
        }
    }
}
