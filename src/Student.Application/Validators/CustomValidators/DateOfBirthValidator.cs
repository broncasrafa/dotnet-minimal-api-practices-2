using FluentValidation;

namespace Student.Application.Validators.CustomValidators;

public static class DateOfBirthValidator 
{
    public static IRuleBuilderOptions<T, DateTime> DateOfBirthValidations<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Date of birth is required")
            .Must(BeAValidDate).WithMessage("Date of birth must be a valid date")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of birth must be less than or equal to the current date");
    }

    private static bool BeAValidDate(DateTime date)
    {
        return date != default(DateTime);
    }
}
