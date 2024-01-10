using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.BaseInterface;

namespace ISSA.Contract.Repository.Infrastructure;
public interface ICacheLayer<T> : IBaseCacheLayer<T> where T : BaseEntity, new()
{

}
