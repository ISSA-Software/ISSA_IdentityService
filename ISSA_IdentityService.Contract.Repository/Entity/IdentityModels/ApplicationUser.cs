using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;

public class ApplicationUser : IdentityUser
{

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsDelete { get; set; } = false;

    [Required]
    public DateTime CreatedTime { get; set; }

    public DateTime? LastUpdatedTime { get; set; }
}