using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISSA.Contract.Repository.Entity.IdentityModels;

namespace ISSA.Repository.Base;
public abstract class BaseDbContext : IdentityDbContext<ApplicationUser>
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {

    }
}
