using Microsoft.AspNetCore.Mvc;

namespace ISSA_IdentityService.Core.QueryObject
{
    public record ApplicationUserQuery : BaseQuery
    {
        [BindProperty(Name = "email", SupportsGet = true)]
        public string? Email { get; init; }
    }
}

