using System.Net;
using Student.Domain.Exceptions.Common;

namespace Student.Domain.Exceptions;

public class UserRegisterErrorException : BaseException
{
    public UserRegisterErrorException(HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base("An error occurred while trying to register the new user", statusCode)
    {
    }

    public UserRegisterErrorException(List<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base($"The following errors occurred while trying to register the new user: {string.Join(", ", errors)}", statusCode)
    {
    }
}
