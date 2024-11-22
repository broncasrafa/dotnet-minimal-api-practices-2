using Student.Application.Services.Interfaces;
using FluentValidation;
using Student.Application.DTO.Request.Course;

namespace Student.Application.Validators.Course;

public class CourseCreateValidation : AbstractValidator<CourseCreateRequest>
{
    private readonly ICourseService _service;

    public CourseCreateValidation(ICourseService courseService)
    {
        _service = courseService;

        RuleFor(model => model.Title).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Course title is required")
            .NotNull().WithMessage("Course title is required")
            .MinimumLength(3).WithMessage($"Course title must at least 3 characters");

        RuleFor(model => model.Credits).Cascade(CascadeMode.Stop)
            .InclusiveBetween(1, 100).WithMessage("Course credits must be 1 between 100");
    }
}
