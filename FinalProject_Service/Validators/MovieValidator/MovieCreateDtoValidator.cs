using FinalProject_Service.Dto.MovieDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.MovieValidator
{
    public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
    {
        public MovieCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(150).WithMessage("Title must be less than 150 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(2000).WithMessage("Description must be less than 2000 characters");

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"Year must be between 1900 and {DateTime.Now.Year}");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10");

            RuleFor(x => x.DirectorId)
                .GreaterThan(0).WithMessage("DirectorId is required");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("At least one Genre must be selected");

            RuleFor(x => x.ActorId)
                .NotEmpty().WithMessage("At least one Actor must be selected");

            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("At least one Language must be selected");

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File == null)
                {
                    context.AddFailure("File", "Poster image is required");
                }
                else
                {
                    if (d.File.Length > 2 * 1024 * 1024)
                        context.AddFailure("File", "Poster image must be less than 2 MB");
                    if (!d.File.ContentType.Contains("image/"))
                        context.AddFailure("File", "Poster file must be an image (jpg/png)");
                }
            });
        }
    }

}
