using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student.Domain.Entities;

namespace Student.Infrastructure.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<SchoolUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Domain.Entities.Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("dbo");

        builder.Entity<Course>().ToTable("Course");
        builder.Entity<Domain.Entities.Student>().ToTable("Student");
        builder.Entity<Enrollment>().ToTable("Enrollment");

        //builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());

        // adicionando o seed de dados
        builder.Seed();
    }
}
