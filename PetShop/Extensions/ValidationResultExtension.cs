using FluentValidation.Results;

namespace PetShop.Extensions;

public static class ValidationResultExtension
{
    public static List<string> GetErrors(this ValidationResult result)
    {
        List<string> errors = new();
        result.Errors.ForEach(p => errors.Add(p.ErrorMessage));
        return errors;
    }
}