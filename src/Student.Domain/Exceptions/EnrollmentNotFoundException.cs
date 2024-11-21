using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class EnrollmentNotFoundException : BaseException
{
    public EnrollmentNotFoundException(int id, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
        : base($"Enrollment with ID: '{id}' was not found", statusCode)
    {
    }
}
