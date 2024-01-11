using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using ISSA_IdentityService.Core.Models.Common;

namespace ISSA_IdentityService.Contract.Service.BaseServiceInterface;
public interface IBaseIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<ApplicationUser?> GetUserByUserNameAsync(string username);


    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<int> DeleteUserAsync(string userId);
}
