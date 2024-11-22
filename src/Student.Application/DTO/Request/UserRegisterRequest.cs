﻿namespace Student.Application.DTO.Request;

public class UserRegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
