using ISSA.Contract.Repository.Entity.IdentityModels;

namespace ISSA.Contract.Repository.Entity
{
    public class Mentor : BaseEntity
    {
        public required string ApplicationUserId { get; set; } = string.Empty;

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
