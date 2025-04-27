using FinalProject_Service.Dto.ActorDtos;
using FluentValidation;

namespace FinalProject_Service.Validators.ActorValidators
{
    public class ActorDeleteDtoValidator : AbstractValidator<ActorDeleteDto>
    {
        public ActorDeleteDtoValidator()
        {
            RuleFor(x => x.Fullname)
                .MaximumLength(30).WithMessage("Fullname must be less than 30 characters");

            RuleFor(x => x.File);

            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File == null)
                {
                    context.AddFailure("Image", "Image is required");
                }
                else
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
