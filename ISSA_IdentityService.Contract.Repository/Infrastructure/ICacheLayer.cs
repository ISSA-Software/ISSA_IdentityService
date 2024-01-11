using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.BaseInterface;

namespace ISSA_IdentityService.Contract.Repository.Infrastructure;
public interface ICacheLayer<T> : IBaseCacheLayer<T> where T : BaseEntity, new()
{

}
