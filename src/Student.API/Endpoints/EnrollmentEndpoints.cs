using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request.Enrollment;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Exceptions;

namespace Student.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/enrollments").WithTags("Enrollments");

        routes.MapGet("/", GetAllEnrollments)
            .WithName("GetAllEnrollments")
            .Produces<ApiResult<IEnumerable<EnrollmentResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de matrículas")
            .WithSummary("Obter a lista de matrículas")
            .WithOpenApi();

        routes.MapGet("/{id:int}", GetOneEnrollment)
            .WithName("GetOneEnrollment")
            .Produces<ApiResult<EnrollmentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter a matrícula pelo ID especificado")
            .WithSummary("Obter a matrícula pelo ID especificado")
            .WithOpenApi();

        routes.MapPost("/", PostEnrollment)
            .Accepts<EnrollmentCreateRequest>("application/json")
            .AddEndpointFilter<ValidationFilter>()
            .WithName("PostEnrollment")
            .Produces<ApiResult<EnrollmentResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar uma nova matrícula")
            .WithSummary("Registrar uma nova matrícula")
            .WithOpenApi();

        routes.MapDelete("/{id:int}", DeleteEnrollment)
            .WithName("DeleteEnrollment")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar a matrícula pelo ID especificado")
            .WithSummary("Deletar a matrícula pelo ID especificado")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> GetAllEnrollments(ILogger<Program> logger, IEnrollmentService service)
    {
        logger.LogInformation("Getting all enrollments");

        var response = await service.GetAllAsync();

        return TypedResults.Ok(ApiResult<IEnumerable<EnrollmentResponse>>.Success(response));
    }
    private async static Task<IResult> GetOneEnrollment(ILogger<Program> logger, IEnrollmentService service, int id)
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
    private async static Task<IResult> PostEnrollment([FromBody] EnrollmentCreateRequest request, ILogger<Program> logger, IEnrollmentService service)
    {
        logger.LogInformation($"Creating new enrollment");

        var response = await service.InsertAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOneEnrollment", routeValues: new { id = response.Id }, value: ApiResult<EnrollmentResponse>.Success(response));
    }
    private async static Task<IResult> DeleteEnrollment(ILogger<Program> logger, IEnrollmentService service, int id)
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
