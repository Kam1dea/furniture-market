using Furniture.Application.Dtos.Product;
using FluentValidation;

namespace Infrastructure.Application.Validators
{
    public class ProductCreateDtoValidator : AbstractValidator<CreateProductDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}