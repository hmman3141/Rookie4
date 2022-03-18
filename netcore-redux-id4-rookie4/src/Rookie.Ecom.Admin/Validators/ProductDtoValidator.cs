using FluentValidation;
using Rookie.Ecom.Business.Interfaces;
using Rookie.Ecom.Business.Services;
using Rookie.Ecom.Contracts.Constants;
using Rookie.Ecom.Contracts.Dtos;

namespace Rookie.Ecom.Admin.Validators
{
    public class ProductDtoValidator : BaseValidator<ProductDto>
    {
        public ProductDtoValidator(IProductService ProductService)
        {
            RuleFor(m => m.Id)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Id)));

            RuleFor(m => m.ProductName)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.ProductName)));

            RuleFor(m => m.ProductName)
               .MaximumLength(ValidationRules.CommonRules.MaxLenghCharactersForText)
               .WithMessage(string.Format(ErrorTypes.Common.MaxLengthError, ValidationRules.CommonRules.MaxLenghCharactersForText))
               .When(m => !string.IsNullOrWhiteSpace(m.ProductName));

            RuleFor(m => m.Price)
                  .NotEmpty()
                  .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.Price)));

            RuleFor(m => m.Price)
               .LessThan(100000000)
               .WithMessage(string.Format("Price must be less than {0}", 100000000))
               .When(m => m.Price > 0);

            RuleFor(m => m.Price)
               .GreaterThan(0)
               .WithMessage(string.Format("Price must be greater than {0}",0))
               .When(m => m.Price < 0);

            RuleFor(m => m.Quantity)
               .GreaterThanOrEqualTo(0)
               .WithMessage(string.Format("Quantity must be greater than or equal to {0}", 0))
               .When(m => m.Quantity < 0);

            RuleFor(m => m.IsFeatured)
                 .NotNull()
                 .WithMessage(x => string.Format(ErrorTypes.Common.RequiredError, nameof(x.IsFeatured)));

            /*RuleFor(x => x).MustAsync(
             async (dto, cancellation) =>
             {
                 var exit = await ProductService.GetByNameAsync(dto.ProductName);
                 return exit == null || exit.Id != dto.Id;
             }
          ).WithMessage("Duplicate record");*/
        }
    }
}
