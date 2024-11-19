using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities;

namespace Student.Infrastructure.Persistence.Context;

internal static class SeedData
{
    public static void Seed(this ModelBuilder builder)
    {
        builder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "Minimal API Development", Credits = 3 },
            new Course { Id = 2, Title = "Spring Boot Development", Credits = 5 },
            new Course { Id = 3, Title = "Ultimate .NET API Development", Credits = 4 }
            );

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Name = "User", NormalizedName = "USER" }
            );
    }
}
