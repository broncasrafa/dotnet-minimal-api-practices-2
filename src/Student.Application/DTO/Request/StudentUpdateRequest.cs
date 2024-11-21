namespace Student.Application.DTO.Request;

public class StudentUpdateRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Picture { get; set; }
    public DateTime DateofBirth { get; set; }
}
