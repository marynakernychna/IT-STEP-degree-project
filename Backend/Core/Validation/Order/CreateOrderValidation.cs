using Core.DTO.Order;
using FluentValidation;

namespace Core.Validation.Order
{
    public class CreateOrderValidation : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderValidation()
        {
            RuleFor(o => o.Address)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(o => o.City)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(o => o.Country)
                .NotEmpty()
                .Length(1, 100);
        }
    }
}
