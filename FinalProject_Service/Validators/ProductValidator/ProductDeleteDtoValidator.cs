using FinalProject_Service.Dto.ProductDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.ProductValidator
{
    public class ProductDeleteDtoValidator : AbstractValidator<ProductDeleteDto>
    {
        public ProductDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.CostPrice)
                .GreaterThan(0).WithMessage("CostPrice must be greater than 0");

            RuleFor(x => x.MovieOrShow)
                .MaximumLength(50).WithMessage("MovieOrShow must be less than 50 characters");
        }
    }

}
