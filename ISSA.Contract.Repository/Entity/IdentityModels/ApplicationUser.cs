using Microsoft.AspNetCore.Identity;


namespace ISSA.Contract.Repository.Entity.IdentityModels;

public class ApplicationUser : IdentityUser
{

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsDelete { get; set; } = false;

    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }
}