using Invedia.DI.Attributes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using ISSA.Contract.Repository.BaseInterface;
using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.Infrastructure;
using ISSA.Repository.Base;

namespace ISSA.Repository.Infrastructure;

[ScopedDependency(ServiceType = typeof(ICacheLayer<>))]
public class CacheLayer<T>(IMemoryCache memoryCache, IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer) : BaseCacheLayer<T>(memoryCache, distributedCache, connectionMultiplexer), IBaseCacheLayer<T>,  ICacheLayer<T> where T : BaseEntity, new()
{

}
