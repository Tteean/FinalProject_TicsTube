using FinalProject_Service.Dto.ProductDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.ProductValidator
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description must be less than 2000 characters");

            RuleFor(x => x.CostPrice)
                .GreaterThan(0).WithMessage("CostPrice must be greater than 0");

            RuleFor(x => x.MovieOrShow)
                .MaximumLength(50).WithMessage("MovieOrShow must be less than 50 characters");

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File != null)
                {
                    if (d.File.Length > 2 * 1024 * 1024)
                        context.AddFailure("File", "Product image must be less than 2 MB");
                    if (!d.File.ContentType.Contains("image/"))
                        context.AddFailure("File", "Product file must be an image (jpg/png)");
                }

                if (string.IsNullOrWhiteSpace(d.MovieOrShow) && d.MovieId == null && d.TVShowId == null)
                {
                    context.AddFailure("MovieOrShow", "You must link Product to a Movie, TV Show or specify MovieOrShow");
                }
            });
        }
    }

}
