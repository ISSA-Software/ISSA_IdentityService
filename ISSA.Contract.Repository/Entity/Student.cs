using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace ISSA_IdentityService.Contract.Repository.Entity
{
    public class Student : BaseEntity
    {
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
