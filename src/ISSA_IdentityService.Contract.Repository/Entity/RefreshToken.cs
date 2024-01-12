using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using System.ComponentModel.DataAnnotations;

namespace ISSA_IdentityService.Contract.Repository.Entity
{
    public class RefreshToken : BaseEntity
    {
        [MaxLength(4096)]
        public string Token { get; set; } = string.Empty;
        public string PreviousToken { get; set; } = string.Empty;
        public string ApplicationUserID { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
        public string IPAdress { get; set; } = string.Empty;
        public string Browser { get; set; } = "Unknow browser";
        public string OS { get; set; } = "Unknow OS";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiredAt { get; set; } = DateTime.UtcNow.AddMinutes(5);

        public ApplicationUser? ApplicationUser { get; set; } = null;

    }
}
