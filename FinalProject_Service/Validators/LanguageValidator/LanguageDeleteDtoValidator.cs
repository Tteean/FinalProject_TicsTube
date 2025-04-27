using FinalProject_Service.Dto.LanguageDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.LanguageValidator
{
    public class LanguageDeleteDtoValidator : AbstractValidator<LanguageDeleteDto>
    {
        public LanguageDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name must be less than 30 characters");

        }
    }
}
