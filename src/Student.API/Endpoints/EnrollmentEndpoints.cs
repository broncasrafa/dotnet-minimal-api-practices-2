using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Exceptions;

namespace Student.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/enrollments").WithTags("Enrollments");

        routes.MapGet("/", GetAll)
            .WithName("GetAll")
            .Produces<ApiResult<IEnumerable<EnrollmentResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de matrículas")
            .WithSummary("Obter a lista de matrículas")
            .WithOpenApi();

        routes.MapGet("/{id:int}", GetOne)
            .WithName("GetOne")
            .Produces<ApiResult<EnrollmentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter a matrícula pelo ID especificado")
            .WithSummary("Obter a matrícula pelo ID especificado")
            .WithOpenApi();

        routes.MapPost("/", Post)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("Post")
            .Produces<ApiResult<EnrollmentResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar uma nova matrícula")
            .WithSummary("Registrar uma nova matrícula")
            .WithOpenApi();

        routes.MapDelete("/{id:int}", Delete)
            .WithName("Delete")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar a matrícula pelo ID especificado")
            .WithSummary("Deletar a matrícula pelo ID especificado")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> GetAll(ILogger<Program> logger, IEnrollmentService service)
    {
        logger.LogInformation("Getting all enrollments");

        var response = await service.GetAllAsync();

        return TypedResults.Ok(ApiResult<IEnumerable<EnrollmentResponse>>.Success(response));
    }
    private async static Task<IResult> GetOne(ILogger<Program> logger, IEnrollmentService service, int id)
    {
        logger.LogInformation($"Getting enrollment with ID: '{id}'");

        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Enrollment ID is required");
        }

        var response = await service.GetByIdAsync(id);
        return TypedResults.Ok(ApiResult<EnrollmentResponse>.Success(response));
    }
    private async static Task<IResult> Post(ILogger<Program> logger, IEnrollmentService service, [FromBody] EnrollmentCreateRequest request)
    {
        logger.LogInformation($"Creating new enrollment");

        var response = await service.InsertAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOne", routeValues: new { id = response.Id }, value: ApiResult<EnrollmentResponse>.Success(response));
    }
    private async static Task<IResult> Delete(ILogger<Program> logger, IEnrollmentService service, int id)
    {
        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Enrollment ID is required");
        }

        logger.LogInformation($"Removing enrollment with ID: '{id}'");

        await service.DeleteAsync(id);

        return TypedResults.Ok(ApiResult<bool>.Success(true));
    }
}
