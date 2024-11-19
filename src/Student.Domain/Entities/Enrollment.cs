﻿using Student.Domain.Entities.Common;

namespace Student.Domain.Entities;

public class Enrollment : BaseEntity
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}