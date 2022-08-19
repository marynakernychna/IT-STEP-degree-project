using Core.DTO;
using FluentValidation;

namespace Core.Validation
{
    public class EntityIdValidation : AbstractValidator<EntityIdDTO>
    {
        public EntityIdValidation()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
