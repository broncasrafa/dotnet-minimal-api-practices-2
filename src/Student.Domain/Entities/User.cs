namespace Student.Domain.Entities;

public class User
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public IList<string> Roles { get; set; }
}
