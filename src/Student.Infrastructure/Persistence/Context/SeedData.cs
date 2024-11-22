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

        builder.Entity<SchoolUser>().HasData(
            new SchoolUser
            {
                Id = "e9b9e0d9-ef83-4704-bc16-b63cdabeece6",
                Email = "schooladmin@localhost.com",
                NormalizedEmail = "SCHOOLADMIN@LOCALHOST.COM",
                UserName = "schooladmin",
                NormalizedUserName = "SCHOOLADMIN",
                FirstName = "School",
                LastName = "Admin",
                PasswordHash = new PasswordHasher<SchoolUser>().HashPassword(null, "Admin123"),
                EmailConfirmed = true
            },
            new SchoolUser
            {
                Id = "f0a6f7af-3143-460f-9d17-cc0261833bc3",
                Email = "schooluser1@localhost.com",
                NormalizedEmail = "SCHOOLUSER1@LOCALHOST.COM",
                UserName = "schooluser1",
                NormalizedUserName = "SCHOOLUSER1",
                FirstName = "School_1",
                LastName = "User",
                PasswordHash = new PasswordHasher<SchoolUser>().HashPassword(null, "User123"),
                EmailConfirmed = true
            },
            new SchoolUser
            {
                Id = "39d9a772-4d54-4ad5-b4b8-cfc9aa24af08",
                Email = "schooluser2@localhost.com",
                NormalizedEmail = "SCHOOLUSER2@LOCALHOST.COM",
                UserName = "schooluser2",
                NormalizedUserName = "SCHOOLUSER2",
                FirstName = "School_2",
                LastName = "User",
                PasswordHash = new PasswordHasher<SchoolUser>().HashPassword(null, "User123"),
                EmailConfirmed = true
            }
        );

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "c3965f85-106b-4ccd-aa72-4c629e0a9976", Name = "Administrator", NormalizedName = "ADMINISTRATOR" },
            new IdentityRole { Id = "345255c0-3e2a-4a4a-b566-5b4cb137d1a2", Name = "User", NormalizedName = "USER" }
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "c3965f85-106b-4ccd-aa72-4c629e0a9976", UserId = "e9b9e0d9-ef83-4704-bc16-b63cdabeece6" },
            new IdentityUserRole<string> { RoleId = "345255c0-3e2a-4a4a-b566-5b4cb137d1a2", UserId = "f0a6f7af-3143-460f-9d17-cc0261833bc3" },
            new IdentityUserRole<string> { RoleId = "345255c0-3e2a-4a4a-b566-5b4cb137d1a2", UserId = "39d9a772-4d54-4ad5-b4b8-cfc9aa24af08" }
        );
    }
}