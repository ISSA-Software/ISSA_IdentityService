using Microsoft.Extensions.Configuration;
using ISSA.Core.Config;

namespace ISSA.Core.Utils;
public class SystemHelper
{
    public static SystemSettingModel Setting => SystemSettingModel.Instance;
    public static IConfiguration Configs => SystemSettingModel.Configs;
    public static string? AppDb => SystemSettingModel.Configs.GetConnectionString("DefaultConnection");
}
