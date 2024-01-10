using Microsoft.EntityFrameworkCore;
using ISSA.Contract.Repository.BaseInterface;
using ISSA.Contract.Repository.Entity;
using ISSA.Contract.Repository.Infrastructure;
using ISSA.Repository.Base;

namespace ISSA.Repository.Infrastructure;
public class Repository<T> : BaseRepository<T>, IRepository<T>, IBaseRepository<T> where T : BaseEntity, new()
{
    public Repository(AppDbContext dbContext) : base(dbContext)
    {

    }
}
