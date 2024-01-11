using AutoMapper;
using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.QueryObject;

namespace ISSA_IdentityService.Service.Services
{
    [ScopedDependency(ServiceType = typeof(IAdminService))]
    public class AdminService(IAdminRepository adminRepository, IMapper mapper, ICacheLayer<Admin> cacheLayer) : BaseService.Service, IAdminService
    {
        public Task<string> CreateAsync(AdminModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Admin>> GetAllAsync(AdminQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Admin?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Admin>> GetPaginatedAsync(AdminQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(string id, AdminModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}