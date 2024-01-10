using AutoMapper;
using Invedia.DI.Attributes;
using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.Infrastructure;
using ISSA.Contract.Repository.Interface;
using ISSA.Contract.Service.Interface;
using ISSA.Core.Models;
using ISSA.Core.Models.Common;
using ISSA.Core.QueryObject;

namespace ISSA.Service.Services
{
    [ScopedDependency(ServiceType = typeof(IMentorService))]
    public class MentorService(IMentorRepository mentorRepository, IMapper mapper, ICacheLayer<Mentor> cacheLayer) : BaseService.Service, IMentorService
    {
        public Task<string> CreateAsync(MentorModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Mentor>> GetAllAsync(MentorQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Mentor?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Mentor>> GetPaginatedAsync(MentorQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(string id, MentorModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
