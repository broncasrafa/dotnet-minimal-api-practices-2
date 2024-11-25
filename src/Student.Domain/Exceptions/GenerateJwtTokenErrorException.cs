using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class GenerateJwtTokenErrorException : BaseException
{
    public GenerateJwtTokenErrorException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) 
        : base($"an error occurred while trying to generate the JWT token: {message}", statusCode)
    {
    }
}
