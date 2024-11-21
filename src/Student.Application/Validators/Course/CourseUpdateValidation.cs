using Student.Application.DTO.Request;
using Student.Application.Services.Interfaces;
using FluentValidation;

namespace Student.Application.Validators.Course;

public class CourseUpdateValidation : AbstractValidator<CourseUpdateRequest>
{
    private readonly ICourseService _courseService;
    public CourseUpdateValidation(ICourseService courseService)
    {
        _courseService = courseService;

        RuleFor(model => model.Id).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Course ID is required")
            .GreaterThan(0).WithMessage("Course ID must be greater than 0");

        RuleFor(model => model.Title).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Course title is required")
            .NotNull().WithMessage("Course title is required")
            .MinimumLength(3).WithMessage($"Course title must at least 3 characters");

        RuleFor(model => model.Credits).Cascade(CascadeMode.Stop)
            .InclusiveBetween(1, 100).WithMessage("Course credits must be 1 between 100");
    }
}
