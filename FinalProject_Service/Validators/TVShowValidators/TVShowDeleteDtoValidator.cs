using FinalProject_Service.Dto.TVShowDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.TVShowValidators
{
    public class TVShowDeleteDtoValidator : AbstractValidator<TVShowDeleteDto>
    {
        public TVShowDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.UtcNow.Year + 1)
                .WithMessage("Year must be a valid year");

            RuleFor(x => x.DirectorId)
                .GreaterThan(0).WithMessage("DirectorId must be greater than 0");

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File != null)
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
