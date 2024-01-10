using Invedia.DI.Attributes;
using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.Interface;
using ISSA.Repository.Infrastructure;

namespace ISSA.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IStudentRepository))]
    public class StudentRepository(AppDbContext dbContext) : Repository<Student>(dbContext), IStudentRepository
    {
    }
}
