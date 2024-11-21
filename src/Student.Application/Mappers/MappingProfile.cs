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
    }
}
