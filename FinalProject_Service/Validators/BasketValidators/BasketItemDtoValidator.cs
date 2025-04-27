using FinalProject_Service.Dto.BasketDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.BasketValidators
{
    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Count)
                .GreaterThan(0).WithMessage("Count must be greater than 0");
        }
    }

}
