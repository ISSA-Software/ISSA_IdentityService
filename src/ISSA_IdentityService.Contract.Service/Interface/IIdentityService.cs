using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using ISSA_IdentityService.Contract.Service.BaseServiceInterface;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.QueryObject;
using System.Security.Claims;

namespace ISSA_IdentityService.Contract.Service.Interface
{
    public interface IIdentityService : IBaseIdentityService
    {
        public Task SetVerifiedPhoneNumberAsync(ApplicationUser user, string phoneNumber);
        public Task<IList<string>?> GetRolesAsync(ApplicationUser user);
        public Task<ApplicationUser> GetUserByIdAsync(string userId);

        public Task AddToRoleAsync(ApplicationUser user, string role);

        public Task SetUserFullNameAsync(ApplicationUser user, string fullName);

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        public Task SetVerfiedEmailAsync(ApplicationUser user, string email);
        public Task SetUserAvatarUrlAsync(ApplicationUser identity, string photoUrl);

        public Task<PaginatedList<ApplicationUser>> GetPaginatedAsync(ApplicationUserQuery query, CancellationToken cancellationToken = default);
    }
}
