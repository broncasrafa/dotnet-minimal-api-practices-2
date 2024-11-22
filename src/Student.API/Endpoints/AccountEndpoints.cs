using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;

namespace Student.API.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/account").WithTags("Account");

        routes.MapPost("/login", LoginUser)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("LoginUser")
            .Accepts<UserLoginRequest>("application/json")
            .Produces<ApiResult<AuthenticatedUserResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Efetuar login")
            .WithSummary("Efetuar login")
            .WithOpenApi();


        routes.MapPost("/register", RegisterUser)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("RegisterUser")
            .Accepts<UserRegisterRequest>("application/json")
            .Produces<ApiResult<string>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar um novo usuário")
            .WithSummary("Registrar um novo usuário")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> LoginUser([FromBody] UserLoginRequest request, ILogger<Program> logger, IAccountService service)
    {
        logger.LogInformation($"Signin user with e-mail: '{request.Email}'");

        var response = await service.LoginAsync(request);
        return TypedResults.Ok(ApiResult<AuthenticatedUserResponse>.Success(response));
    }

    private async static Task<IResult> RegisterUser([FromBody] UserRegisterRequest request, ILogger<Program> logger, IAccountService service)
    {
        logger.LogInformation($"Register new user with e-mail: '{request.Email}'");

        var response = await service.RegisterAsync(request);
        return TypedResults.Ok(ApiResult<string>.Success(response));
    }
}
