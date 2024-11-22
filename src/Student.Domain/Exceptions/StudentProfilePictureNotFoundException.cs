using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class StudentProfilePictureNotFoundException : BaseException
{
    public StudentProfilePictureNotFoundException(int studentId, Guid fileId, HttpStatusCode statusCode = HttpStatusCode.NotFound)
        : base($"The specified profile picture with ID: '{fileId.ToString()}' does not exist for student ID: '{studentId}'.", statusCode)
    {
    }
}
