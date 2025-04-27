using FinalProject_Service.Dto.BasketDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.BasketValidators
{
    public class CheckoutItemDtoValidator : AbstractValidator<CheckoutItemDto>
    {
        public CheckoutItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0");

            RuleFor(x => x.TotalItemPrice)
                .GreaterThan(0).WithMessage("TotalItemPrice must be greater than 0");
        }
    }

}
