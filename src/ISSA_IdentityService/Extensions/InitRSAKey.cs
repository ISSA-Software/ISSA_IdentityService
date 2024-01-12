using ISSA_IdentityService.Core.Config;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace ISSA_IdentityService.Extensions
{
    public class InitRSAKey
    {
        public static void Init()
        {
            if (SystemSettingModel.Configs == null)
            {
                throw new Exception("Jwt:ValidIssuer is null");
            }
            string private_key = "";
            string public_key = "";
            try
            {
                private_key = File.ReadAllText(SystemSettingModel.Configs["Jwt:PrivateKey"] ?? string.Empty);
                public_key = File.ReadAllText(SystemSettingModel.Configs["Jwt:PublicKey"] ?? string.Empty);
                if (private_key != "" && public_key != "")
                {
                    var private_rsa = RSA.Create();
                    private_rsa.ImportFromPem(private_key);
                    SystemSettingModel.RSAPrivateKey = new RsaSecurityKey(private_rsa);

                    var public_rsa = RSA.Create();
                    public_rsa.ImportFromPem(public_key);
                    SystemSettingModel.RSAPublicKey = new RsaSecurityKey(public_rsa);

                }
            }
            catch (Exception e)
            {
                if (SystemSettingModel.Environment == "Development") // "Development
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine(e);
                    Console.Error.WriteLine("Can't read private_key.pem or public_key.pem");
                }
            }
            finally
            {
                if (SystemSettingModel.RSAPrivateKey == null || SystemSettingModel.RSAPublicKey == null)
                {
                    if (SystemSettingModel.Environment == "Development")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Fallback to Hmac HS256 alg");
                        Console.ResetColor();

                    }
                }
            }
        }
    }
}
