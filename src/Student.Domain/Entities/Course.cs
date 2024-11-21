using Student.Domain.Entities.Common;

namespace Student.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; }
    public int Credits { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
