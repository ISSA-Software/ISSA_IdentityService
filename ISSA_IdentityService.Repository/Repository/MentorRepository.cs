using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Repository.Infrastructure;

namespace ISSA_IdentityService.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IMentorRepository))]
    public class MentorRepository(AppDbContext dbContext) : Repository<Mentor>(dbContext), IMentorRepository
    {

    }
}
