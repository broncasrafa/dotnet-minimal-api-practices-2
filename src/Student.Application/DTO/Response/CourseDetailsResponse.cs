namespace Student.Application.DTO.Response;

public class CourseDetailsResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public List<StudentResponse> Students { get; set; } = new List<StudentResponse>();
}
