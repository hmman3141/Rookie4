using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class OrderDtoValidator : BaseValidator<OrderDto>
    {
        public OrderDtoValidator(IOrderService OrderService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.TotalAmount)
               .GreaterThanOrEqualTo(0)
               .WithMessage(string.Format("Price must be greater than or equal to {0}", 0))
               .When(m => m.TotalAmount < 0);

            RuleFor(m => m.ShippingAddress)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.ShippingAddress)));

            /*RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await OrderService.GetByNameAsync(dto.OrderName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");*/
        }
    }
}
