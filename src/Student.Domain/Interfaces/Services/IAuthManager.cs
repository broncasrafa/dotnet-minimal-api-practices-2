using Microsoft.AspNetCore.Identity;
using Student.Domain.Entities;

namespace Student.Domain.Interfaces.Services;

public interface IAuthManager
{
    Task<User> LoginAsync(string email, string password);
    Task<IEnumerable<IdentityError>> RegisterAsync(string firstName, string lastName, string password, string email, DateTime? dateOfBirth);
    Task<SchoolUser> GetUserByEmailAsync(string email);
}
