using Microsoft.AspNetCore.Mvc;

namespace Student.API.Middlewares;

public class AuthErrorHandler
{
    public static async Task HandleAuthError(HttpContext context, int statusCode)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Instance = context.Request.Path,
            Title = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "You are not authenticated.",
                StatusCodes.Status403Forbidden => "Access denied.",
                _ => "Authentication error."
            },
            Detail = statusCode switch
            {
                StatusCodes.Status401Unauthorized => "Please send a valid token in the Authorization header of the request.",
                StatusCodes.Status403Forbidden => "You do not have permission to access this resource.",
                _ => "Please contact support for more information."
            }
        };

        // Log opcional
        var logger = context.RequestServices.GetRequiredService<ILogger<AuthErrorHandler>>();
        logger.LogError("Auth Error ({StatusCode}): {Path}", statusCode, context.Request.Path);

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
