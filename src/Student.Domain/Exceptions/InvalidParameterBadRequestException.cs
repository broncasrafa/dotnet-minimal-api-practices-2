using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class InvalidParameterBadRequestException : BaseException
{
    public InvalidParameterBadRequestException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }
}
