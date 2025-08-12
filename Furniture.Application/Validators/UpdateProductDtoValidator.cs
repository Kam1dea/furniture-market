using Furniture.Application.Dtos.Product;
using FluentValidation;

namespace Infrastructure.Application.Validators;

public class UpdateProductDtoValidator: AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Color)
            .MaximumLength(30).WithMessage("Color must not exceed 30 characters.");
        RuleFor(x => x.Material)
            .MaximumLength(50).WithMessage("Material must not exceed 50 characters.");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}