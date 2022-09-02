using Core.DTO.Order;
using FluentValidation;

namespace Core.Validation.Order
{
    public class ChangeOrderValidation : AbstractValidator<ChangeOrderInfoDTO>
    {
        public ChangeOrderValidation()
        {
            RuleFor(o => o.OrderInfo.Address)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(o => o.OrderInfo.City)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(o => o.OrderInfo.Country)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(o => o.OrderId)
                .GreaterThanOrEqualTo(1);
        }
    }
}
