using Microsoft.AspNetCore.Mvc;
using Student.API.Filters;
using Student.API.Models;
using Student.Application.DTO.Request.Student;
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
            .WithDescription("Obter os detalhes do aluno com os cursos matriculados pelo ID especificado")
            .WithSummary("Obter os detalhes do aluno com os cursos matriculados pelo ID especificado")
            .WithOpenApi();

        routes.MapGet("/{id:int}/picture/download/{fileId}", GetProfilePicture)
            .WithName("GetProfilePicture")
            .Produces<ApiResult<Domain.Models.FileResult>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Download da foto de um aluno pelo ID especificado e pelo ID do arquivo especificado")
            .WithSummary("Download da foto de um aluno pelo ID especificado e pelo ID do arquivo especificado")
            .WithOpenApi();

        routes.MapPost("/{id:int}/picture/upload", PostProfilePicture)
            .WithName("PostProfilePicture")
            .Accepts<StudentUploadImageRequest>("multipart/form-data")
            .Produces<ApiResult<bool>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithDescription("Upload da foto de um novo aluno pelo ID especificado")
            .WithSummary("Upload da foto de um novo aluno pelo ID especificado")
            .DisableAntiforgery()
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

    private async static Task<IResult> PostProfilePicture(IFormFile file, int id, ILogger<Program> logger, IStudentService service)
    {
        if (id < 1)
        {
            logger.LogWarning($"Invalid Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Student ID is required");
        }

        logger.LogInformation($"Uploading profile picture student with ID: '{id}'");

        var fileId = await service.UploadProfilePictureAsync(new StudentUploadImageRequest { ProfilePicture = file });

        if (!string.IsNullOrWhiteSpace(fileId))
            await service.UpdatePictureIdAsync(id, fileId);

        return TypedResults.Ok(ApiResult<bool>.Success(true));
    }

    private async static Task<IResult> GetProfilePicture(int id, Guid fileId, IStudentService service, ILogger<Program> logger)
    {
        if (id < 1)
        {
            logger.LogWarning($"Invalid student ID Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("Student ID is required");
        }
        var isValidFileId = Guid.TryParse(fileId.ToString(), out _);
        if (!isValidFileId)
        {            
            logger.LogWarning($"Invalid file ID Parameter with value: '{id}'");
            throw new InvalidParameterBadRequestException("File ID is in invalid format. File ID must be a GUID");
        }

        logger.LogInformation($"Downloading profile picture student with ID: '{id}' and file ID: '{isValidFileId.ToString()}'");

        var fileResponse = await service.DownloadProfilePictureAsync(id, fileId);

        return Results.File(fileResponse.Stream, fileResponse.ContentType);
    }
}
