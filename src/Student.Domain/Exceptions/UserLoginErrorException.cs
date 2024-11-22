using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class UserLoginErrorException : BaseException
{
    public UserLoginErrorException(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base("Incorrect username or password", statusCode)
    {
    }
}
