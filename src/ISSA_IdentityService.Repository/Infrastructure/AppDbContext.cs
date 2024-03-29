﻿using System.Reflection;
using Invedia.DI.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ISSA_IdentityService.Core.Utils;
using ISSA_IdentityService.Repository.Base;

namespace ISSA_IdentityService.Repository.Infrastructure;
//[ScopedDependency(ServiceType = typeof(AppDbContext))]
public sealed partial class AppDbContext : BaseDbContext
{
    public readonly int CommandTimeoutInSecond = 20 * 60;

    public static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(
            builder => builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole());

    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(CommandTimeoutInSecond);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {


            var connectionString = SystemHelper.AppDb;

            optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(
                    typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name);

                sqlServerOptionsAction.MigrationsHistoryTable("Migration");
            });
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
