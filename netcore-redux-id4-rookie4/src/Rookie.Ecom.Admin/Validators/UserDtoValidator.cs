using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class UserDtoValidator : BaseValidator<UserDto>
    {
        public UserDtoValidator(IUserService UserService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.FirstName)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.FirstName)));

            RuleFor(m => m.FirstName)
               .MaximumLength(ValidationRules.CommonRules.MaxLenghCharactersForText)
               .WithMessage(string.Format(ErrorTypes.Common.MaxLengthError, ValidationRules.CommonRules.MaxLenghCharactersForText))
               .When(m => !string.IsNullOrWhiteSpace(m.FirstName));

            RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await UserService.GetByNameAsync(dto.FirstName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");
        }
    }
}
