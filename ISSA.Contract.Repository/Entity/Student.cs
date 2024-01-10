using ISSA.Contract.Repository.Entity.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace ISSA.Contract.Repository.Entity
{
    public class Student : BaseEntity
    {
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
