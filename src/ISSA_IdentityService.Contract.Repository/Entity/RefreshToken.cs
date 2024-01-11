using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;

namespace ISSA_IdentityService.Contract.Repository.Entity
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = string.Empty;
        public string PreviousToken { get; set; } = string.Empty;
        public string ApplicationUserID { get; set; } = string.Empty;
        public string SessionID { get; set; } = string.Empty;
        public string IPAdress { get; set; } = string.Empty;
        public string Browser { get; set; } = "Unknow browser";
        public string OS { get; set; } = "Unknow OS";

        public ApplicationUser? ApplicationUser { get; set; } = null;

    }
}
