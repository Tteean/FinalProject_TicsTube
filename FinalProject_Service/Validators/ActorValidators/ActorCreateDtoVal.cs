using FinalProject_Service.Dto.ActorDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Service.Validators.ActorValidators
{
    public class ActorCreateDtoVal : AbstractValidator<ActorCreateDto>
    {
        public ActorCreateDtoVal()
        {
            RuleFor(x => x.Fullname)
                .MaximumLength(30).WithMessage("fullname must be less than 30 characters");
            RuleFor(x => x.File); 
            RuleFor(d => d).Custom((d, context) =>
            {
                if (d.File == null)
                {
                    context.AddFailure("Image", "Image is required");
                }
                if (d.File.Length > 2 * 1024 * 1024)
                {
                    context.AddFailure("Image", "Image must be less than 2 mb");
                }
                if (!d.File.ContentType.Contains("image/"))
                {
                    context.AddFailure("Image", "Image must be jpg or png");
                }
            });
        }
    }
}
