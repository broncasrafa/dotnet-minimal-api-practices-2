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

        routes.MapGet("/", GetAllStudents)
            .WithName("GetAllStudents")
            .Produces<ApiResult<IEnumerable<StudentResponse>>>(StatusCodes.Status200OK)
            .WithDescription("Obter a lista de alunos")
            .WithSummary("Obter a lista de alunos")
            .WithOpenApi();

        routes.MapGet("/{id:int}", GetOneStudent)
            .WithName("GetOneStudent")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter o aluno pelo ID especificado")
            .WithSummary("Obter o aluno pelo ID especificado")
        .WithOpenApi();

        routes.MapGet("/{id:int}/details", GetStudentDetails)
            .WithName("GetStudentDetails")
            .Produces<ApiResult<StudentDetailsResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Obter os detalhes do aluno pelo ID especificado")
            .WithSummary("Obter os detalhes do aluno pelo ID especificado")
        .WithOpenApi();

        routes.MapPost("/", PostStudent)            
            .WithName("PostStudent")
            .Accepts<StudentCreateRequest>("application/json")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .AddEndpointFilter<ValidationFilter>()
            .WithDescription("Registrar um novo aluno")
            .WithSummary("Registrar um novo aluno")
            .WithOpenApi();

        routes.MapPut("/{id:int}", PutStudent)
            .AddEndpointFilter<ValidationFilter>()
            .WithName("PutStudent")
            .Accepts<StudentUpdateRequest>("application/json")
            .Produces<ApiResult<StudentResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Atualizar os dados do aluno pelo ID especificado")
            .WithSummary("Atualizar os dados do aluno pelo ID especificado")
            .WithOpenApi();

        routes.MapDelete("/{id:int}", DeleteStudent)
            .WithName("DeleteStudent")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Deletar o aluno pelo ID especificado")
            .WithSummary("Deletar o aluno pelo ID especificado")
            .WithOpenApi();

        return routes;
    }


    private async static Task<IResult> GetAllStudents(ILogger<Program> logger, IStudentService service)
    {
        logger.LogInformation("Getting all students");

        var response = await service.GetAllAsync();

        return TypedResults.Ok(ApiResult<IEnumerable<StudentResponse>>.Success(response));
    }
    private async static Task<IResult> GetOneStudent(ILogger<Program> logger, IStudentService service, int id)
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
    private async static Task<IResult> GetStudentDetails(ILogger<Program> logger, IStudentService service, int id)
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
    private async static Task<IResult> PostStudent([FromBody] StudentCreateRequest request, ILogger<Program> logger, IStudentService service)
    {
        logger.LogInformation($"Creating new student with name: '{request.FirstName} {request.LastName}'");

        var response = await service.InsertAsync(request);
        return TypedResults.CreatedAtRoute(routeName: "GetOneStudent", routeValues: new { id = response.Id }, value: ApiResult<StudentResponse>.Success(response));
    }
    private async static Task<IResult> PutStudent([FromBody] StudentUpdateRequest request, ILogger<Program> logger, IStudentService service, int id)
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
    private async static Task<IResult> DeleteStudent(ILogger<Program> logger, IStudentService service, int id)
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
