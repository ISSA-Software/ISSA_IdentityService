namespace ISSA_IdentityService.Core.Models.Common
{
    public class AuthenticateResponse
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }
}
