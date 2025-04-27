using FinalProject_Service.Dto.MovieDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.MovieValidator
{
    public class MovieDeleteDtoValidator : AbstractValidator<MovieDeleteDto>
    {
        public MovieDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

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
        }
    }

}
