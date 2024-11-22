using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Student.Application.DTO.Response;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Services;

namespace Student.Infrastructure.Services;

internal class AuthManager : IAuthManager
{
    private readonly ILogger<AuthManager> _logger;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly UserManager<SchoolUser> _userManager;

    public AuthManager(
        ILogger<AuthManager> logger, 
        IJwtTokenService jwtTokenService, 
        UserManager<SchoolUser> userManager)
    {
        _logger = logger;
        _jwtTokenService = jwtTokenService;
        _userManager = userManager;
    }

    public async Task<SchoolUser> GetUserByEmailAsync(string email) 
        => await _userManager.FindByEmailAsync(email);

    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return default;

        var isValidCredentials = await _userManager.CheckPasswordAsync(user, password);
        if (!isValidCredentials) return default;

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var token = _jwtTokenService.GenerateTokenJwt(user, roleClaims, userClaims?.ToList());

        return new User
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            Roles = roles
        };
    }

    public async Task<IEnumerable<IdentityError>> RegisterAsync(string firstName, string lastName, string password, string email, DateTime? dateOfBirth)
    {
        var user = new SchoolUser
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            UserName = email
        };

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }
}
/*

 */