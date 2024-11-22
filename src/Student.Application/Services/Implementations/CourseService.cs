using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using Student.Domain.Entities;
using Student.Domain.Extensions;
using Student.Domain.Exceptions;
using Student.Domain.Interfaces.Repositories;
using AutoMapper;
using Student.Application.DTO.Request.Course;

namespace Student.Application.Services.Implementations;

internal class CourseService : ICourseService
{
    private readonly ILogger<CourseService> _logger;
    private readonly IMapper _mapper;
    private readonly ICourseRepository _repository;

    public CourseService(ILogger<CourseService> logger, IMapper mapper, ICourseRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<IEnumerable<CourseResponse>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CourseResponse>>(data);
    }
    public async Task<CourseResponse> GetByIdAsync(int id)
    {
        var data = await _repository.GetByIdAsync(id)
                                    .OrElseThrowsAsync(new CourseNotFoundException(id));
        return _mapper.Map<CourseResponse>(data);
    }

    public async Task<CourseDetailsResponse> GetCourseStudentsAsync(int courseId)
    {
        var data = await _repository.GetStudentListAsync(courseId);
        return _mapper.Map<CourseDetailsResponse>(data);
    }
    public async Task<CourseResponse> FindByAsync(Expression<Func<Course, bool>> predicate)
    {
        var data = await _repository.FirstOrDefaultAsync(predicate);
        return _mapper.Map<CourseResponse>(data);
    }
    public async Task<CourseResponse> InsertAsync(CourseCreateRequest request)
    {
        var entity = _mapper.Map<Course>(request);
        // entity.CreatedBy = "";
        var newCourse = await _repository.InsertAsync(entity);

        return _mapper.Map<CourseResponse>(newCourse);
    }
    public async Task<CourseResponse> UpdateAsync(CourseUpdateRequest request)
    {
        var currentCourse = await _repository.GetByIdAsync(request.Id)
                                             .OrElseThrowsAsync(new CourseNotFoundException(request.Id));
        currentCourse.Title = request.Title;
        currentCourse.Credits = request.Credits;
        // currentCourse.CreatedBy = request.CreatedBy;
        currentCourse.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(currentCourse);

        return _mapper.Map<CourseResponse>(currentCourse);
    }
    public async Task DeleteAsync(int id)
    {
        var currentCourse = await _repository.GetByIdAsync(id)
                                             .OrElseThrowsAsync(new CourseNotFoundException(id));
       
        await _repository.DeleteAsync(currentCourse);
    }

    
}
