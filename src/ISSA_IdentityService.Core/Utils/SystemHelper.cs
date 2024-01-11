using Microsoft.Extensions.Configuration;
using ISSA_IdentityService.Core.Config;

namespace ISSA_IdentityService.Core.Utils;
public class SystemHelper
{
    public static SystemSettingModel Setting => SystemSettingModel.Instance;
    public static IConfiguration Configs => SystemSettingModel.Configs;
    public static string? AppDb => SystemSettingModel.Configs.GetConnectionString("DefaultConnection");
}
