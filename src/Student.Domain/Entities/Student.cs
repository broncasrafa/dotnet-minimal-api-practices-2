using Student.Domain.Entities.Common;

namespace Student.Domain.Entities;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NumberId { get; set; }
    public string Picture { get; set; }
    public DateTime DateofBirth { get; set; }
}
