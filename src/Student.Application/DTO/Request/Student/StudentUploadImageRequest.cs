using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Student.Application.DTO.Request.Student;

public class StudentUploadImageRequest
{
    [Required(ErrorMessage = "Student profile picture is required")]
    public IFormFile ProfilePicture { get; set; }
}
