using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Models.Common;

namespace ISSA_IdentityService.Contract.Service.Interface
{
    public interface IAuthenticateService 
    {
        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateModel model, CancellationToken cancellationToken = default);
        Task SignOutAsync(CancellationToken cancellationToken = default);
        Task<AuthenticateResponse> RefreshTokenAsync(AuthenticateModel model, CancellationToken cancellationToken = default);
        Task SignOutAllAsync(CancellationToken cancellationToken = default);
    }
}
