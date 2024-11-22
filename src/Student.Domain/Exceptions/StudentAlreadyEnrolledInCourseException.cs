using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class StudentAlreadyEnrolledInCourseException : BaseException
{
    public StudentAlreadyEnrolledInCourseException(int studentId, int courseId, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base($"Student with ID: '{studentId}' already enrolled in course ID: '{courseId}'", statusCode)
    {
    }
}
