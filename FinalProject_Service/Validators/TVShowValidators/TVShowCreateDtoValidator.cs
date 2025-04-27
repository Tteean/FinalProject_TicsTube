using FinalProject_Service.Dto.TVShowDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.TVShowValidators
{
    public class TVShowCreateDtoValidator : AbstractValidator<TVShowCreateDto>
    {
        public TVShowCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must be less than 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 1)
                .WithMessage("Year must be a valid year");

            RuleFor(x => x.DirectorId)
                .GreaterThan(0).WithMessage("DirectorId must be greater than 0");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("At least one genre must be selected");

            RuleFor(x => x.ActorId)
                .NotEmpty().WithMessage("At least one actor must be selected");

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("At least one language must be selected");

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File == null)
                {
                    context.AddFailure("Image", "Image is required");
                }
                else
                {
                    if (d.File.Length > 2 * 1024 * 1024)
                    {
                        context.AddFailure("Image", "Image must be less than 2 MB");
                    }

                    if (!d.File.ContentType.Contains("image/"))
                    {
                        context.AddFailure("Image", "Image must be jpg or png");
                    }
                }
            });
        }
    }

}
