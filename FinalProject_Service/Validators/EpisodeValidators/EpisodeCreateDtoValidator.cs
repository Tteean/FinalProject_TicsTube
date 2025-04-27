using FinalProject_Service.Dto.EpisodeDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.EpisodeValidators
{
    public class EpisodeCreateDtoValidator : AbstractValidator<EpisodeCreateDto>
    {
        public EpisodeCreateDtoValidator()
        {
            RuleFor(x => x.TVShowId)
                .NotNull().WithMessage("TVShowId is required");

            RuleFor(x => x.SeasonId)
                .NotNull().WithMessage("SeasonId is required");

            RuleFor(x => x.EpisodeNumber)
                .GreaterThan(0).WithMessage("EpisodeNumber must be greater than 0");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(150).WithMessage("Title must be less than 150 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must be less than 1000 characters");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0");

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File == null)
                {
                    context.AddFailure("File", "Image is required");
                }
                else
                {
                    if (d.File.Length > 2 * 1024 * 1024)
                        context.AddFailure("File", "Image must be less than 2 MB");
                    if (!d.File.ContentType.Contains("image/"))
                        context.AddFailure("File", "File must be an image (jpg/png)");
                }

            });
        }
    }

}
