namespace Student.Application.DTO.Request;

public class StudentCreateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NumberId { get; set; }
    public string Picture { get; set; }
    public DateTime DateofBirth { get; set; }
}
