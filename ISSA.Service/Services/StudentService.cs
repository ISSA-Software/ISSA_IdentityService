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
    [ScopedDependency(ServiceType = typeof(IStudentService))]
    public class StudentService(IStudentRepository studentRepository, IMapper mapper, ICacheLayer<Student> cacheLayer) : BaseService.Service, IStudentService
    {
       
        public Task<string> CreateAsync(StudentModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Student>> GetAllAsync(StudentQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Student?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<Student>> GetPaginatedAsync(StudentQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(string id, StudentModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
