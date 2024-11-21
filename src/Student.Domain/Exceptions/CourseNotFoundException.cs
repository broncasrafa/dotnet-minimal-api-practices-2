using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class CourseNotFoundException : BaseException
{
    public CourseNotFoundException(int id, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
        : base($"Course with ID: '{id}' was not found", statusCode)
    {
    }
}
