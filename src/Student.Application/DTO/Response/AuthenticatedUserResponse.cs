namespace Student.Application.DTO.Response;

public class AuthenticatedUserResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
