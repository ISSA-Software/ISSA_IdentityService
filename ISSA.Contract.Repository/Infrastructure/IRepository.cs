using ISSA.Contract.Repository.BaseInterface;

namespace ISSA.Contract.Repository.Infrastructure;
public interface IRepository<T> : IBaseRepository<T> where T : Entity.BaseEntity, new()
{

}
