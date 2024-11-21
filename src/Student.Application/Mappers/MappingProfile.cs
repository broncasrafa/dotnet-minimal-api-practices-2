using Student.Application.DTO.Request;
using Student.Application.DTO.Response;
using Student.Domain.Entities;
using AutoMapper;

namespace Student.Application.Mappers;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseResponse>().ReverseMap();
        CreateMap<CourseCreateRequest, Course>().ReverseMap();
        CreateMap<CourseUpdateRequest, Course>().ReverseMap();
        CreateMap<Course, CourseDetailsResponse>()
            .ForMember(c => c.Students, x => x.MapFrom(course => course.Enrollments.Select(student => student.Student)));

        CreateMap<Domain.Entities.Student, StudentResponse>().ReverseMap();
        CreateMap<StudentCreateRequest, Domain.Entities.Student>().ReverseMap();
        CreateMap<StudentUpdateRequest, Domain.Entities.Student>().ReverseMap();
        CreateMap<Student.Domain.Entities.Student, StudentDetailsResponse>()
            .ForMember(c => c.Courses, x => x.MapFrom(student => student.Enrollments.Select(course => course.Course)));

        CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();
        CreateMap<EnrollmentCreateRequest, Enrollment>().ReverseMap();
    }
}
