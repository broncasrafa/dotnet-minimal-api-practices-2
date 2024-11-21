using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class StudentNotFoundException : BaseException
{
    public StudentNotFoundException(int id, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : base($"Student with ID: '{id}' was not found", statusCode)
    {
    }
}
