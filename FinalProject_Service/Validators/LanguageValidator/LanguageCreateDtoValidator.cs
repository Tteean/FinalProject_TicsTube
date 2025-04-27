using FinalProject_Service.Dto.LanguageDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.LanguageValidator
{
    public class LanguageCreateDtoValidator : AbstractValidator<LanguageCreateDto>
    {
        public LanguageCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(20).WithMessage("Name must be less than 30 characters");

        }
    }
}
