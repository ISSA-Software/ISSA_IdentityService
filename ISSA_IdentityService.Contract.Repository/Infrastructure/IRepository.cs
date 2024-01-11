using ISSA_IdentityService.Contract.Repository.BaseInterface;

namespace ISSA_IdentityService.Contract.Repository.Infrastructure;
public interface IRepository<T> : IBaseRepository<T> where T : Entity.BaseEntity, new()
{

}
