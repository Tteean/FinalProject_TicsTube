using FinalProject_Service.Dto.BasketDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.BasketValidators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required");
        }
    }

}
