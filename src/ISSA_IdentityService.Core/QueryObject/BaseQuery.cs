using Microsoft.AspNetCore.Mvc;

namespace ISSA_IdentityService.Core.QueryObject;
public record class BaseQuery
{
    [BindProperty(Name = "page", SupportsGet = true)]
    public int Page { get; init; } = 1;
    [BindProperty(Name = "limit", SupportsGet = true)]
    public int Limit { get; init; } = 200;
    //[BindProperty(Name = "sort", SupportsGet = true)]
    //public string? Sort { get; init; }
    [BindProperty(Name = "is-deleted", SupportsGet = true)]
    public bool IsDeleted { get; init; } = false;
    [BindProperty(Name = "create-from", SupportsGet = true)]
    public DateTime? CreateFrom { get; init; }
    [BindProperty(Name = "create-to", SupportsGet = true)]
    public DateTime? CreateTo { get; init; }
    [BindProperty(Name = "update-from", SupportsGet = true)]
    public DateTime? UpdateFrom { get; init; }
    [BindProperty(Name = "update-to", SupportsGet = true)]
    public DateTime? UpdateTo { get; init; }
    [BindProperty(Name = "delete-from", SupportsGet = true)]
    public DateTime? DeleteFrom { get; init; }
    [BindProperty(Name = "delete-to", SupportsGet = true)]
    public DateTime? DeleteTo { get; init; }
}
