using FinalProject_Service.Dto.GenreDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.GenreValidators
{
    public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
    {
        public CreateGenreDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name must be less than 30 characters");

        }
    }
}
