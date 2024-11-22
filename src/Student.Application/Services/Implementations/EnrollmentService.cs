using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Entities;
using Student.Domain.Extensions;
using Student.Domain.Exceptions;
using Student.Domain.Interfaces.Repositories;
using AutoMapper;

namespace Student.Application.Services.Implementations;

internal class EnrollmentService : IEnrollmentService
{
    private readonly ILogger<EnrollmentService> _logger;
    private readonly IMapper _mapper;
    private readonly IEnrollmentRepository _repository;

    public EnrollmentService(ILogger<EnrollmentService> logger, IMapper mapper, IEnrollmentRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<IEnumerable<EnrollmentResponse>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<EnrollmentResponse>>(data);
    }
    public async Task<IEnumerable<EnrollmentResponse>> GetStudentEnrollmentsAsync(int studentId)
    {
        var data = await _repository.GetStudentEnrollmentsAsync(studentId);
        return _mapper.Map<IEnumerable<EnrollmentResponse>>(data);
    }
    public async Task<EnrollmentResponse> GetByIdAsync(int id)
    {
        var data = await _repository.GetByIdAsync(id)
                                    .OrElseThrowsAsync(new EnrollmentNotFoundException(id));
        return _mapper.Map<EnrollmentResponse>(data);
    }
    public async Task<EnrollmentResponse> FindByAsync(Expression<Func<Enrollment, bool>> predicate)
    {
        var data = await _repository.FirstOrDefaultAsync(predicate);
        return _mapper.Map<EnrollmentResponse>(data);
    }
    public async Task<EnrollmentResponse> InsertAsync(EnrollmentCreateRequest request)
    {
        var entity = _mapper.Map<Enrollment>(request);
        // entity.CreatedBy = "";

        // verificar se o aluno já pertence ao curso em questão para DEPOIS inserir
        var studentEnrolledCourses = await _repository.FindByAsync(c => c.StudentId == request.StudentId);
        var enrolledCourse = studentEnrolledCourses?.FirstOrDefault(c => c.CourseId == request.CourseId);
        if (enrolledCourse != null) throw new StudentAlreadyEnrolledInCourseException(request.StudentId, request.CourseId);

        var newEnrollment = await _repository.InsertAsync(entity);

        return _mapper.Map<EnrollmentResponse>(newEnrollment);
    }
    public async Task DeleteAsync(int id)
    {
        var currentEnrollment = await _repository.GetByIdAsync(id)
                                             .OrElseThrowsAsync(new EnrollmentNotFoundException(id));
       
        await _repository.DeleteAsync(currentEnrollment);
    }
}
