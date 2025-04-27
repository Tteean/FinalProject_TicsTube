using FinalProject_Service.Dto.MovieDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.MovieValidator
{
    public class MovieCommentCreateDtoValidator : AbstractValidator<MovieCommentCreateDto>
    {
        public MovieCommentCreateDtoValidator()
        {
            RuleFor(x => x.MovieId)
                .GreaterThan(0).WithMessage("MovieId must be greater than 0");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required")
                .MaximumLength(500).WithMessage("Text must be less than 500 characters");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status must be a valid enum value");
        }
    }

}
