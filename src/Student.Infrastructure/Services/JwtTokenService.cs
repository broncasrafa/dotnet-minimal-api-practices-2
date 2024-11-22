using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Services;
using Student.Infrastructure.Settings;

namespace Student.Infrastructure.Services;

internal class JwtTokenService(ILogger<JwtTokenService> _logger, IOptions<JWTSettings> jwtSettings) : IJwtTokenService
{
    private readonly JWTSettings _jwtSettings = jwtSettings.Value;

    public string GenerateTokenJwt(SchoolUser user, List<Claim> roleClaims, List<Claim> userClaims)
    {
        _logger.LogInformation($"Generating JWT token for user: '{user.Email}'");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("userId", user.Id),
        }.Union(userClaims)
         .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpiresInDays),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken jwt = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwt);
    }
}
