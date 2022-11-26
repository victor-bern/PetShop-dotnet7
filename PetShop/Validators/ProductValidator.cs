using FluentValidation;
using PetShop.Domain.Entities;

namespace PetShop.Validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Title).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
    }
}