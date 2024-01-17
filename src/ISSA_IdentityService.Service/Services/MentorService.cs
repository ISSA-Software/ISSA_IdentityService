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
    [ScopedDependency(ServiceType = typeof(IMentorService))]
    public class MentorService(IMentorRepository repository, IMapper mapper, IIdentityService service, ICacheLayer<Mentor> cacheLayer) : BaseService.Service, IMentorService
    {
        public async Task<string> CreateAsync(MentorModel model, CancellationToken cancellationToken = default)
        {
            var Mentor = mapper.Map<Mentor>(model);
            var entity = await repository.AddAsync(Mentor, cancellationToken);
            return entity.Id;
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await repository.GetSingleAsync(x => x.Id == id, cancellationToken);

            if (entity != null)
            {
                _ = service.DeleteUserAsync(entity.ApplicationUserId);
                var i = await repository.DeleteAsync(x => x.Id == entity.Id, cancellationToken);
                return i;
            }
            return 0;
        }

        public Task<ICollection<Mentor>> GetAllAsync(MentorQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Mentor?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var Mentor = await repository.GetSingleAsync(x => x.Id == id, cancellationToken);
            return Mentor;
        }

        public async Task<PaginatedList<Mentor>> GetPaginatedAsync(MentorQuery query, CancellationToken cancellationToken = default)
        {
            var Mentors = await repository.GetAsync(null, cancellationToken);
            var paginatedList = await Mentors.PaginatedListAsync(query);
            return paginatedList;
        }

        public async Task<int> UpdateAsync(string id, MentorModel model, CancellationToken cancellationToken = default)
        {
            var Mentor = mapper.Map<Mentor>(model);
            Mentor.Id = id;
            int i = await repository.UpdateAsync(Mentor, cancellationToken);
            return i;
        }
    }
}