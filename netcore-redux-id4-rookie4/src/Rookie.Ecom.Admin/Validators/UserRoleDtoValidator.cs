using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class UserRoleDtoValidator : BaseValidator<UserRoleDto>
    {
        public UserRoleDtoValidator(IUserRoleService UserRoleService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            /*RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await UserRoleService.GetByNameAsync(dto.FirstName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");*/
        }
    }
}
