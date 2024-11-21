using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories.Common;

namespace Student.Infrastructure.Persistence.Repositories;

internal class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
