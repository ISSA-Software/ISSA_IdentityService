namespace ISSA_IdentityService.Core.Models.Common
{
    public class AuthenticateResponse
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public object User { get; set; } = new object();
        public string[] Roles { get; set; } = [];
    }
}
