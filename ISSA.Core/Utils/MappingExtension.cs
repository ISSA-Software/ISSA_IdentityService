using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ISSA.Core.Models.Common;
using ISSA.Core.QueryObject;

namespace ISSA.Core.Utils;
public static class MappingExtension
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, BaseQuery query) where TDestination : class
    => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), query);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}
