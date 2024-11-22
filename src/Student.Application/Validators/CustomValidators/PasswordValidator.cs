using FluentValidation;

namespace Student.Application.Validators.CustomValidators;

public static class PasswordValidator
{
    public static IRuleBuilderOptions<T, string> PasswordValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 5)
    {
        return ruleBuilder
                        .NotNull().WithMessage("Password is required")
                        .NotEmpty().WithMessage("Password is required")
                        .MinimumLength(minimumLength).WithMessage($"Password must have at least {minimumLength} characters")
                        .Matches("[A-Z]").WithMessage("Password must have at least 1 uppercase letter")
                        .Matches("[a-z]").WithMessage("Password must have at least 1 lowercase letter")
                        .Matches("[0-9]").WithMessage("Password must have at least 1 number")
                        .Matches("[!*@#$%^&+=]").WithMessage("Password must have at least 1 special character"); // /^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[^\w\s]).{8,}$/
    }

    public static IRuleBuilderOptions<T, string> PasswordConfirmationValidations<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
    {
        return ruleBuilder
                .NotNull().WithMessage("Confirmation Password is required")
                .NotEmpty().WithMessage("Confirmation Password is required")
                .MinimumLength(minimumLength).WithMessage($"Confirmation Password must have at least {minimumLength} characters")
                .Matches("[A-Z]").WithMessage("Confirmation Password must have at least 1 uppercase letter")
                .Matches("[a-z]").WithMessage("Confirmation Password must have at least 1 lowercase letter")
                .Matches("[0-9]").WithMessage("Confirmation Password must have at least 1 number")
                .Matches("[!*@#$%^&+=]").WithMessage("Confirmation Password must have at least 1 special character");
    }
}
