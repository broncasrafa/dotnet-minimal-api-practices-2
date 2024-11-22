using Microsoft.Extensions.Logging;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Interfaces.Services;
using Student.Domain.Extensions;
using Student.Domain.Exceptions;
using AutoMapper;
using Student.Application.DTO.Request.Account;

namespace Student.Application.Services.Implementations;

internal class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly IMapper _mapper;
    private readonly IAuthManager _authManager;

    public AccountService(ILogger<AccountService> logger, IMapper mapper, IAuthManager authManager)
    {
        _logger = logger;
        _mapper = mapper;
        _authManager = authManager;
    }

    public async Task<bool> CheckIfEmailAlreadyExistsAsync(string email)
        => await _authManager.GetUserByEmailAsync(email) != null;

    public async Task<AuthenticatedUserResponse> LoginAsync(UserLoginRequest request)
    {
        var user = await _authManager.LoginAsync(request.Email, request.Password)
                                        .OrElseThrowsAsync(new UserLoginErrorException());

        return new AuthenticatedUserResponse { UserId = user.UserId, Email = user.Email, Token = user.Token };
    }

    public async Task<string> RegisterAsync(UserRegisterRequest request)
    {
        var result = await _authManager.RegisterAsync(request.FirstName, request.LastName, request.Password, request.Email, request.DateOfBirth);
        if (result.Any())
        {
            var errors = result.Select(err => err.Description).ToList();
            throw new UserRegisterErrorException(errors);
        }

        return "User registered successfully";
    }
}

