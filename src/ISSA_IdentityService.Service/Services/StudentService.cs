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
    [ScopedDependency(ServiceType = typeof(IStudentService))]
    public class StudentService(IStudentRepository repository, IMapper mapper, IIdentityService service, ICacheLayer<Student> cacheLayer) : BaseService.Service, IStudentService
    {
        public async Task<string> CreateAsync(StudentModel model, CancellationToken cancellationToken = default)
        {
            var Student = mapper.Map<Student>(model);
            var entity = await repository.AddAsync(Student, cancellationToken);
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

        public Task<ICollection<Student>> GetAllAsync(StudentQuery query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Student?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var Student = await repository.GetSingleAsync(x => x.Id == id, cancellationToken);
            return Student;
        }

        public async Task<PaginatedList<Student>> GetPaginatedAsync(StudentQuery query, CancellationToken cancellationToken = default)
        {
            var Students = await repository.GetAsync(x => x.IsDelete == query.IsDeleted, cancellationToken);
            var paginatedList = await Students.PaginatedListAsync(query);
            return paginatedList;
        }

        public async Task<int> UpdateAsync(string id, StudentModel model, CancellationToken cancellationToken = default)
        {
            var Student = mapper.Map<Student>(model);
            int i = await repository.UpdateAsync(Student, cancellationToken);
            return i;
        }
    }
}