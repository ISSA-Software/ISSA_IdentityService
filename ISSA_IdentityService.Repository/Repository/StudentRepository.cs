using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Repository.Infrastructure;

namespace ISSA_IdentityService.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IStudentRepository))]
    public class StudentRepository(AppDbContext dbContext) : Repository<Student>(dbContext), IStudentRepository
    {
    }
}
