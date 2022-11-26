using FluentValidation;
using PetShop.Dto;

namespace PetShop.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Please inform a name");
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Please inform a birthdate");
    }
}