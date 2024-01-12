using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Repository.Infrastructure;

namespace ISSA_IdentityService.Repository.Repository
{
    [ScopedDependency(ServiceType = typeof(IRefreshTokenRepository))]
    public class RefreshTokenRepositor (AppDbContext dbContext) : Repository<RefreshToken>(dbContext), IRefreshTokenRepository
    {

    }
}
