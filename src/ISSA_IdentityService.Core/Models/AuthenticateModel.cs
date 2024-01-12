using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ISSA_IdentityService.Core.Models
{
    public class AuthenticateModel
    {
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsAnonymous { get; set; } = false;
        public string? SessionId { get; set; } = string.Empty;
        public string? IPAdress { get; set; }
        public string? Browser { get; set; }
        public string? OS { get; set; }
    }
}
