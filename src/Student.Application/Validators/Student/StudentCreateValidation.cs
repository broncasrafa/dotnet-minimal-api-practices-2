using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Student.Application.Validators.CustomValidators;
using Student.Application.DTO.Request.Student;

namespace Student.Application.Validators.Student;

public class StudentCreateValidation : AbstractValidator<StudentCreateRequest>
{
    public StudentCreateValidation()
    {
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