using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class OrderDetailDtoValidator : BaseValidator<OrderDetailDto>
    {
        public OrderDetailDtoValidator(IOrderDetailService OrderDetailService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.Price)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Price)));

            RuleFor(m => m.Price)
               .LessThan(100000000)
               .WithMessage(string.Format("Price must be less than {0}", 100000000))
               .When(m => m.Price > 0);

            RuleFor(m => m.Price)
               .GreaterThanOrEqualTo(0)
               .WithMessage(string.Format("Price must be greater than {0}", 0))
               .When(m => m.Price < 0);

            RuleFor(m => m.Quantity)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Quantity)));

            RuleFor(m => m.Quantity)
               .GreaterThan(0)
               .WithMessage(string.Format("Quantity must be greater than {0}", 0))
               .When(m => m.Quantity < 0);

            /*RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await OrderDetailService.GetByNameAsync(dto.OrderDetailName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");*/
        }
    }
}
