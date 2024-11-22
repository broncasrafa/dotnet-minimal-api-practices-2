using Student.Application.DTO.Request;
using Student.Application.DTO.Response;

namespace Student.Application.Services.Interfaces;

public interface IAccountService
{
    Task<bool> CheckIfEmailAlreadyExistsAsync(string email);
    Task<AuthenticatedUserResponse> LoginAsync(UserLoginRequest reques);
    Task<string> RegisterAsync(UserRegisterRequest request);
}
