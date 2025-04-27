using FinalProject_Service.Dto.BasketDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.BasketValidators
{
    public class CheckoutDtoValidator : AbstractValidator<CheckoutDto>
    {
        public CheckoutDtoValidator()
        {
            RuleFor(x => x.OrderDto)
                .NotNull().WithMessage("Order information is required")
                .SetValidator(new OrderDtoValidator());

            RuleFor(x => x.CheckoutItemDtos)
                .NotEmpty().WithMessage("Checkout items are required");

            RuleForEach(x => x.CheckoutItemDtos)
                .SetValidator(new CheckoutItemDtoValidator());
        }
    }

}
