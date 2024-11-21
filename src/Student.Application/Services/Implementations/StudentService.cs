using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Interfaces.Repositories;
using AutoMapper;
using Student.Domain.Entities;
using Student.Domain.Exceptions;
using Student.Domain.Extensions;

namespace Student.Application.Services.Implementations;

internal class StudentService : IStudentService
{
    private readonly ILogger<StudentService> _logger;
    private readonly IMapper _mapper;
    private readonly IStudentRepository _repository;

    public StudentService(ILogger<StudentService> logger, IMapper mapper, IStudentRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }



    public async Task<IEnumerable<StudentResponse>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentResponse>>(data);
    }
    public async Task<StudentResponse> GetByIdAsync(int id)
    {
        var data = await _repository.GetByIdAsync(id)
                                    .OrElseThrowsAsync(new StudentNotFoundException(id));
        return _mapper.Map<StudentResponse>(data);
    } 
    public async Task<StudentDetailsResponse> GetStudentDetailsAsync(int id)
    {
        var data = await _repository.GetStudentDetailsAsync(id)
                                    .OrElseThrowsAsync(new StudentNotFoundException(id));
        return _mapper.Map<StudentDetailsResponse>(data);
    }
    public async Task<StudentResponse> FindByAsync(Expression<Func<Domain.Entities.Student, bool>> predicate)
    {
        var data = await _repository.FirstOrDefaultAsync(predicate);
        return _mapper.Map<StudentResponse>(data);
    }
    public async Task<StudentResponse> InsertAsync(StudentCreateRequest request)
    {
        var entity = _mapper.Map<Domain.Entities.Student>(request);
        // entity.CreatedBy = "";
        var newStudent = await _repository.InsertAsync(entity);

        return _mapper.Map<StudentResponse>(newStudent);
    }
    public async Task<StudentResponse> UpdateAsync(StudentUpdateRequest request)
    {
        var currentStudent = await _repository.GetByIdAsync(request.Id)
                                             .OrElseThrowsAsync(new StudentNotFoundException(request.Id));
        currentStudent.FirstName = request.FirstName;
        currentStudent.LastName = request.LastName;
        currentStudent.DateOfBirth = request.DateofBirth;
        currentStudent.Picture = request.Picture;
        // currentStudent.UpdatedBy = request.UpdatedBy;
        currentStudent.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(currentStudent);

        return _mapper.Map<StudentResponse>(currentStudent);
    }
    public async Task DeleteAsync(int id)
    {
        var currentStudent = await _repository.GetByIdAsync(id)
                                             .OrElseThrowsAsync(new StudentNotFoundException(id));

        await _repository.DeleteAsync(currentStudent);
    }
}
