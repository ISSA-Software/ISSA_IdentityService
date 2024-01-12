namespace ISSA_IdentityService.Core.Models
{
    public class AuthenticationRequest
    {
        public string? IdToken { get; set; }
        public string? RefreshToken { get; set; }
        public bool IsAnonymous { get; set; } = false;
    }
}
