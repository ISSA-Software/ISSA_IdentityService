using Invedia.DI.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using ISSA_IdentityService.Contract.Repository.BaseInterface;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Repository.Base;

namespace ISSA_IdentityService.Repository.Infrastructure;

[ScopedDependency(ServiceType = typeof(ICacheLayer<>))]
public class CacheLayer<T>(IMemoryCache memoryCache, IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer) : BaseCacheLayer<T>(memoryCache, distributedCache, connectionMultiplexer), IBaseCacheLayer<T>,  ICacheLayer<T> where T : BaseEntity, new()
{

}
