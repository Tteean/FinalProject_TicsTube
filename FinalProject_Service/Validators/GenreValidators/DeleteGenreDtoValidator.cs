using FinalProject_Service.Dto.GenreDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.GenreValidators
{
    public class DeleteGenreDtoValidator : AbstractValidator<DeleteGenreDto>
    {
        public DeleteGenreDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name must be less than 30 characters");

        }
    }
}
