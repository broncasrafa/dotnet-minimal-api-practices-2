using Student.Application.DTO.Response;
using Student.Application.Services.Interfaces;
using FluentValidation;
using Student.Domain.Entities;
using Student.Application.DTO.Request.Enrollment;


namespace Student.Application.Validators.Enrollment;

public class EnrollmentCreateValidation : AbstractValidator<EnrollmentCreateRequest>
{
    private readonly IEnrollmentService _service;
    public EnrollmentCreateValidation(IEnrollmentService service)
    {
        _service = service;

        RuleFor(model => model.CourseId).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Course ID is required")
            .GreaterThan(0).WithMessage("Course ID must be greater than 0");

        RuleFor(model => model.StudentId).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Student ID is required")
            .GreaterThan(0).WithMessage("Course ID must be greater than 0")
            .MustAsync(CheckIfStudentIsAlreadyRegisteredInCourseAsync).WithMessage("Student already enrolled in this course");
    }

    private async Task<bool> CheckIfStudentIsAlreadyRegisteredInCourseAsync(EnrollmentCreateRequest request, int studentId, CancellationToken cancellationToken)
    {
        var enrollments = await _service.GetStudentEnrollmentsAsync(request.StudentId);
        if (enrollments is null) return true;

        var enrolledCourse = enrollments.SingleOrDefault(c => c.CourseId == request.CourseId);
        if (enrolledCourse is null) return true;
        return false;
    }
}