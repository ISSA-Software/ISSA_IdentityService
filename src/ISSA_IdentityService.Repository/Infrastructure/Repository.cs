using Microsoft.EntityFrameworkCore;
using ISSA_IdentityService.Contract.Repository.BaseInterface;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Repository.Base;

namespace ISSA_IdentityService.Repository.Infrastructure;
public class Repository<T> : BaseRepository<T>, IRepository<T>, IBaseRepository<T> where T : BaseEntity, new()
{
    public Repository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
