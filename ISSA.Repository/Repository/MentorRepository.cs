using Invedia.DI.Attributes;
using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.Interface;
using ISSA.Repository.Infrastructure;

namespace ISSA.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IMentorRepository))]
    public class MentorRepository(AppDbContext dbContext) : Repository<Mentor>(dbContext), IMentorRepository
    {

    }
}
