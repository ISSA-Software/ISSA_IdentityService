using AutoMapper;
using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.QueryObject;
using ISSA_IdentityService.Core.Utils;

namespace ISSA_IdentityService.Service.Services
{
    [ScopedDependency(ServiceType = typeof(IAdminService))]
    public class AdminService(IAdminRepository adminRepository, IMapper mapper, ICacheLayer<Admin> cacheLayer) : BaseService.Service, IAdminService
    {
        public async Task<string> CreateAsync(AdminModel model, CancellationToken cancellationToken = default)
        {
            var admin = mapper.Map<Admin>(model);
            var entity = await adminRepository.AddAsync(admin, cancellationToken);
            return entity.Id;
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var affectedRows = await adminRepository.DeleteAsync(x => x.Id == id, cancellationToken);
            return affectedRows;
        }

        public Task<ICollection<Admin>> GetAllAsync(AdminQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Admin?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var admin = await adminRepository.GetSingleAsync(x => x.Id == id, cancellationToken);
            return admin;
        }

        public async Task<PaginatedList<Admin>> GetPaginatedAsync(AdminQuery query, CancellationToken cancellationToken = default)
        {
            var admins = await adminRepository.GetAsync(null, cancellationToken);
            var paginatedList = await admins.PaginatedListAsync(query);
            return paginatedList;
        }

        public async Task<int> UpdateAsync(string id, AdminModel model, CancellationToken cancellationToken = default)
        {
            var admin = mapper.Map<Admin>(model);
            int i = await adminRepository.UpdateAsync(admin, cancellationToken);
            return i;
        }
    }
}