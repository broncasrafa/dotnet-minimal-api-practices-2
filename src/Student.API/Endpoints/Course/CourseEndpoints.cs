using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Exceptions;

namespace Student.API.Endpoints.Course;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/courses").WithTags("Courses");

        routes.MapGet("/", CourseEndpoints.GetAll)
            .WithName("GetAll")
            .Produces<ApiResult<IEnumerable<CourseResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de cursos")
            .WithSummary("Obter a lista de cursos")
            .WithOpenApi();

        routes.MapGet("/{id:int}", CourseEndpoints.GetOne)
            .WithName("GetOne")
            .Produces<ApiResult<CourseResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter o curso pelo ID especificado")
            .WithSummary("Obter o curso pelo ID especificado")
            .WithOpenApi();

        routes.MapPost("/", CourseEndpoints.Post)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("Post")
            .Produces<ApiResult<CourseResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar um novo curso")
            .WithSummary("Registrar um novo curso")
            .WithOpenApi();

        routes.MapPut("/{id:int}", CourseEndpoints.Put)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("Put")
            .Produces<ApiResult<CourseResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Atualizar os dados do curso pelo ID especificado")
            .WithSummary("Atualizar os dados do curso pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:int}", CourseEndpoints.Delete)
            .WithName("Put")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar o curso pelo ID especificado")
            .WithSummary("Deletar o curso pelo ID especificado")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> GetAll(ILogger<Program> logger, ICourseService service) 
    {
        logger.LogInformation("Getting all courses");

        var response = await service.GetAllAsync();

        return TypedResults.Ok(ApiResult<IEnumerable<CourseResponse>>.Success(response));
    }
    private async static Task<IResult> GetOne(ILogger<Program> logger, ICourseService service, int id)
    {
        logger.LogInformation($"Getting coupon with id: '{id}'");

        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Course ID is required");
        }

        var response = await service.GetByIdAsync(id);
        return TypedResults.Ok(ApiResult<CourseResponse>.Success(response));
    }
    private async static Task<IResult> Post(ILogger<Program> logger, ICourseService service, [FromBody] CourseCreateRequest request)
    {
        logger.LogInformation($"Creating new course with title: '{request.Title}'");

        var response = await service.InsertAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOne", routeValues: new { id = response.Id }, value: ApiResult<CourseResponse>.Success(response));
    }
    private async static Task<IResult> Put(ILogger<Program> logger, ICourseService service, int id, [FromBody] CourseUpdateRequest request)
    {
        if (id < 1 || request.Id < 1 || (id != request.Id))
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}', does not match with body id value: '{request.Id}'");
            throw new InvalidParameterBadRequestException($"Course ID with value: '{id}', does not match with body ID value: '{request.Id}'");
        }

        logger.LogInformation($"Updating course with ID: '{id}'");

        var response = await service.UpdateAsync(request);
        return TypedResults.Ok(ApiResult<CourseResponse>.Success(response));
    }
    private async static Task<IResult> Delete(ILogger<Program> logger, ICourseService service, int id)
    {
        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Course ID is required");
        }

        logger.LogInformation($"Removing course with ID '{id}'");

        await service.DeleteAsync(id);

        return TypedResults.Ok(ApiResult<bool>.Success(true));
    }
}
