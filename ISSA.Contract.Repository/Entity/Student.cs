using ISSA.Contract.Repository.Entity.IdentityModels;

namespace ISSA.Contract.Repository.Entity
{
    public class Student : BaseEntity
    {
        public required string ApplicationUserId { get; set; } = string.Empty;

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
