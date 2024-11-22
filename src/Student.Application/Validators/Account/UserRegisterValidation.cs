using Student.Application.DTO.Request;
using Student.Application.Services.Interfaces;
using Student.Application.Validators.CustomValidators;
using FluentValidation;

namespace Student.Application.Validators.Account;

public class UserRegisterValidation : AbstractValidator<UserRegisterRequest>
{
    private readonly IAccountService _accountService;

    public UserRegisterValidation(IAccountService accountService)
    {
        _accountService = accountService;

        RuleFor(c => c.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("FirstName is required")
            .NotNull().WithMessage("FirstName must not be null")
            .MinimumLength(3).WithMessage("FirstName must have at least 3 characters");

        RuleFor(c => c.LastName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("LastName is required")
            .NotNull().WithMessage("LastName must not be null")
            .MinimumLength(3).WithMessage("LastName must have at least 3 characters");

        RuleFor(c => c.Password).Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Password must not be null")
            .NotEmpty().WithMessage("Password is required")
            .PasswordValidations();

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
           .EmailValidations()
           .MustAsync(VerificarSeJaExisteEmailAsync).WithMessage("E-mail already registered");

        RuleFor(c => c.DateOfBirth).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Date of birth is required")
            .Must(BeAValidDate).WithMessage("Date of birth must be a valid date")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of birth must be less than or equal to the current date");
    }

    private async Task<bool> VerificarSeJaExisteEmailAsync(string email, CancellationToken cancellationToken)
    {
        var exists = !await _accountService.CheckIfEmailAlreadyExistsAsync(email);
        return exists;
    }

    private bool BeAValidDate(DateTime? date)
    {
        return date != default(DateTime);
    }
}
