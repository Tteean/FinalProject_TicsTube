using FinalProject_Service.Dto.SeasonDtos;
using FinalProject_Service.Validators.EpisodeValidators;
using FluentValidation;

namespace FinalProject_Service.Validators.SeasonValidators
{
    public class SeasonCreateDtoValidator : AbstractValidator<SeasonCreateDto>
    {
        public SeasonCreateDtoValidator()
        {
            RuleFor(x => x.TVShowId)
                .GreaterThan(0).WithMessage("TVShowId must be greater than 0");

            RuleFor(x => x.SeasonNumber)
                .GreaterThan(0).WithMessage("SeasonNumber must be greater than 0");

            RuleForEach(x => x.Episodes)
                .SetValidator(new EpisodeCreateDtoValidator())
                .When(x => x.Episodes != null && x.Episodes.Count != 0);
        }
    }
}
