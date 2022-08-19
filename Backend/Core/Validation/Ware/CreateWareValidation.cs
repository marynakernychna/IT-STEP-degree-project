using Core.DTO.Ware;
using FluentValidation;

namespace Core.Validation.Ware
{
    public class CreateWareValidation : AbstractValidator<CreateWareDTO>
    {
        public CreateWareValidation()
        {
            RuleFor(ware => ware.Title)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(ware => ware.Description)
                .NotEmpty()
                .Length(1, 1000);

            RuleFor(ware => ware.Cost)
                .NotEmpty()
                .InclusiveBetween(0, 100000);

            RuleFor(ware => ware.AvailableCount)
                .NotEmpty()
                .InclusiveBetween(1, 10000);

            RuleFor(ware => ware.PhotoBase64)
                .NotEmpty();

            RuleFor(ware => ware.PhotoExtension)
                .NotEmpty();

            RuleFor(ware => ware.CategoryTitle)
                .NotEmpty();
        }
    }
}
