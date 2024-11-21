namespace Student.Application.DTO.Response;

public class EnrollmentResponse
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    public virtual CourseResponse Course { get; set; }
    public virtual StudentResponse Student { get; set; }
}
