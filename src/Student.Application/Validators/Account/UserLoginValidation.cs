using Student.Application.DTO.Request;
using Student.Application.Validators.CustomValidators;
using Student.Application.Services.Interfaces;
using FluentValidation;

namespace Student.Application.Validators.Account;

public class UserLoginValidation : AbstractValidator<UserLoginRequest>
{
    private readonly IAccountService _accountService;

    public UserLoginValidation(IAccountService accountService)
    {
        _accountService = accountService;

        RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
           .EmailValidations();

        RuleFor(c => c.Password)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("Password is required");
    }    
}
