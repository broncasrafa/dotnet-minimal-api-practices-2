namespace Student.Application.DTO.Response;

public class StudentDetailsResponse : StudentResponse
{
    public List<CourseResponse> Courses { get; set; } = new List<CourseResponse>();
}
