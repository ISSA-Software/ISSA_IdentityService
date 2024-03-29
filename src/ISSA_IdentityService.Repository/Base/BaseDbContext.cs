﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;

namespace ISSA_IdentityService.Repository.Base;
public abstract class BaseDbContext : IdentityDbContext<ApplicationUser>
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {

    }
}
