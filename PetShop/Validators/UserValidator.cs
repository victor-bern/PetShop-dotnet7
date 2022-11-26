using FluentValidation;
using PetShop.Domain.Entities;

namespace PetShop.Validators;


public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please inform a name");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().WithMessage("Please inform a password");
        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Please inform a birthdate");
    }
}