using System.Security.Claims;
using Student.Domain.Entities;

namespace Student.Domain.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateTokenJwt(SchoolUser user, List<Claim> roleClaims, List<Claim> userClaims);
}
