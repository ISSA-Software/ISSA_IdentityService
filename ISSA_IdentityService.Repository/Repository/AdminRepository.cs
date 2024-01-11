using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Repository.Infrastructure;

namespace ISSA_IdentityService.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IAdminRepository))]
    public class AdminRepository(AppDbContext dbContext) : Repository<Admin>(dbContext), IAdminRepository
    {

    }
}
