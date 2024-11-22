using Student.Application.DTO.Request;
using FluentValidation;
using Student.Application.Validators.CustomValidators;

namespace Student.Application.Validators.Student;

public class StudentUpdateValidation : AbstractValidator<StudentUpdateRequest>
{
    public StudentUpdateValidation()
    {
        RuleFor(model => model.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Student ID is required")
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(c => c.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("FirstName is required")
            .NotNull().WithMessage("FirstName must not be null")
            .MinimumLength(3).WithMessage("FirstName must have at least 3 characters");

        RuleFor(c => c.LastName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("LastName is required")
            .NotNull().WithMessage("LastName must not be null")
            .MinimumLength(3).WithMessage("LastName must have at least 3 characters");

        RuleFor(c => c.DateofBirth).Cascade(CascadeMode.Stop)
            .DateOfBirthValidations();
    }
}
