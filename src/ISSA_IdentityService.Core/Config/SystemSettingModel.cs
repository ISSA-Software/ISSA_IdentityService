﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace ISSA_IdentityService.Core.Config;
public class SystemSettingModel
{
    public static SystemSettingModel? Instance { get; set; }

    public static IConfiguration? Configs { get; set; }
    public string ApplicationName { get; set; } = Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty;
    public static SecurityKey? RSAPrivateKey { get; set; }
    public static SecurityKey? RSAPublicKey { get; set; }
    public static string? Environment { get; set; }

    public string? Domain { get; set; }
}
