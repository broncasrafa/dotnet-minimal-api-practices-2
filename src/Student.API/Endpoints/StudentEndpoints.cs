using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Exceptions;

namespace Student.API.Endpoints;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder builder)
    {
        var routes = builder.MapGroup("api/students").WithTags("Students");

        routes.MapGet("/", GetAll)
            .WithName("GetAll")
            .Produces<ApiResult<IEnumerable<StudentResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de alunos")
            .WithSummary("Obter a lista de alunos")
            .WithOpenApi();

        routes.MapGet("/{id:int}", GetOne)
            .WithName("GetOne")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter o aluno pelo ID especificado")
            .WithSummary("Obter o aluno pelo ID especificado")
        .WithOpenApi();

        routes.MapGet("/{id:int}/details", GetDetails)
            .WithName("GetDetails")
            .Produces<ApiResult<StudentDetailsResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter os detalhes do aluno pelo ID especificado")
            .WithSummary("Obter os detalhes do aluno pelo ID especificado")
        .WithOpenApi();

        routes.MapPost("/", Post)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("Post")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Registrar um novo aluno")
            .WithSummary("Registrar um novo aluno")
            .WithOpenApi();

        routes.MapPut("/{id:int}", Put)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("Put")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Atualizar os dados do aluno pelo ID especificado")
            .WithSummary("Atualizar os dados do aluno pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:int}", Delete)
            .WithName("Delete")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar o aluno pelo ID especificado")
            .WithSummary("Deletar o aluno pelo ID especificado")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> GetAll(ILogger<Program> logger, IStudentService service)
    {
        logger.LogInformation("Getting all students");

        var response = await service.GetAllAsync();

        return TypedResults.Ok(ApiResult<IEnumerable<StudentResponse>>.Success(response));
    }
    private async static Task<IResult> GetOne(ILogger<Program> logger, IStudentService service, int id)
    {
        logger.LogInformation($"Getting student with ID: '{id}'");

        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Student ID is required");
        }

        var response = await service.GetByIdAsync(id);
        return TypedResults.Ok(ApiResult<StudentResponse>.Success(response));
    }
    private async static Task<IResult> GetDetails(ILogger<Program> logger, IStudentService service, int id)
    {
        logger.LogInformation($"Getting student with ID: '{id}'");

        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Student ID is required");
        }

        var response = await service.GetStudentDetailsAsync(id);
        return TypedResults.Ok(ApiResult<StudentDetailsResponse>.Success(response));
    }
    private async static Task<IResult> Post(ILogger<Program> logger, IStudentService service, [FromBody] StudentCreateRequest request)
    {
        logger.LogInformation($"Creating new student with name: '{request.FirstName} {request.LastName}'");

        var response = await service.InsertAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOne", routeValues: new { id = response.Id }, value: ApiResult<StudentResponse>.Success(response));
    }
    private async static Task<IResult> Put(ILogger<Program> logger, IStudentService service, int id, [FromBody] StudentUpdateRequest request)
    {
        if (id < 1 || request.Id < 1 || (id != request.Id))
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}', does not match with body id value: '{request.Id}'");
            throw new InvalidParameterBadRequestException($"Student ID with value: '{id}', does not match with body ID value: '{request.Id}'");
        }

        logger.LogInformation($"Updating student with ID: '{id}'");

        var response = await service.UpdateAsync(request);
        return TypedResults.Ok(ApiResult<StudentResponse>.Success(response));
    }
    private async static Task<IResult> Delete(ILogger<Program> logger, IStudentService service, int id)
    {
        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Student ID is required");
        }

        logger.LogInformation($"Removing student with ID: '{id}'");

        await service.DeleteAsync(id);

        return TypedResults.Ok(ApiResult<bool>.Success(true));
    }
}
