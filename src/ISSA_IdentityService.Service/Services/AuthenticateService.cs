using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;

namespace ISSA_IdentityService.Service.Services
{
    [ScopedDependency(ServiceType = typeof(IAuthenticateService))]
    public class AuthenticateService (IRefreshTokenRepository repository, ICacheLayer<RefreshToken>  cacheLayer) : BaseService.Service, IAuthenticateService
    {

    }
}
