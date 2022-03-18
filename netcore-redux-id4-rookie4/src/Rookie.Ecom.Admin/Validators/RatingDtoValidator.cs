using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class RatingDtoValidator : BaseValidator<RatingDto>
    {
        public RatingDtoValidator(IRatingService RatingService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.Rate)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Rate)));

            RuleFor(m => m.Rate)
               .GreaterThan(0)
               .WithMessage(string.Format("Rating must be greater than {0}", 0))
               .When(m => m.Rate < 0);

            RuleFor(m => m.Comment)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Comment)));

            RuleFor(m => m.Comment)
               .MaximumLength(ValidationRules.CommonRules.MaxLenghCharactersForText)
               .WithMessage(string.Format(ErrorTypes.Common.MaxLengthError, ValidationRules.CommonRules.MaxLenghCharactersForText))
               .When(m => !string.IsNullOrWhiteSpace(m.Comment));
            /*RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await RatingService.GetByNameAsync(dto.RatingName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");*/
        }
    }
}
