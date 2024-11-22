using Student.Domain.Entities;
using Student.Application.DTO.Response;
using Student.Application.DTO.Request.Course;
using Student.Application.DTO.Request.Enrollment;
using Student.Application.DTO.Request.Student;
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

        CreateMap<Domain.Entities.Student, StudentResponse>()
            .ForMember(dest => dest.PictureId, opt => opt.MapFrom(src => src.Picture))
            .ReverseMap();
        CreateMap<StudentCreateRequest, Domain.Entities.Student>().ReverseMap();
        CreateMap<StudentUpdateRequest, Domain.Entities.Student>().ReverseMap();
        CreateMap<Domain.Entities.Student, StudentDetailsResponse>()
            .ForMember(c => c.Courses, x => x.MapFrom(student => student.Enrollments.Select(course => course.Course)));

        CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();
        CreateMap<EnrollmentCreateRequest, Enrollment>().ReverseMap();
    }
}
