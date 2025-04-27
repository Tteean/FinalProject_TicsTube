using FinalProject_Service.Dto.BasketDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.BasketValidators
{
    public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required")
                .MaximumLength(100).WithMessage("ProductName must be less than 100 characters");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0");

            RuleFor(x => x.PricePerItem)
                .GreaterThan(0).WithMessage("PricePerItem must be greater than 0");
        }
    }

}
