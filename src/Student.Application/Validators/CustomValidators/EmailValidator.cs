using FluentValidation;

namespace Student.Application.Validators.CustomValidators;

public static class EmailValidator
{
    public static IRuleBuilderOptions<T, string> EmailValidations<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
                .NotNull().WithMessage("E-mail must not be null")
                .NotEmpty().WithMessage("E-mail is required")
                .MaximumLength(200).WithMessage("E-mail must not exceed 200 characters")
                .EmailAddress();
    }
}
