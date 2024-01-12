using FirebaseAdmin;
using FirebaseAdmin.Auth;
using IdentityModel;
using Invedia.DI.Attributes;
using ISSA_IdentityService.Contract.Repository.Entity;
using ISSA_IdentityService.Contract.Repository.Entity.IdentityModels;
using ISSA_IdentityService.Contract.Repository.Infrastructure;
using ISSA_IdentityService.Contract.Repository.Interface;
using ISSA_IdentityService.Contract.Service.Interface;
using ISSA_IdentityService.Core.Config;
using ISSA_IdentityService.Core.Models;
using ISSA_IdentityService.Core.Models.Common;
using ISSA_IdentityService.Core.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ISSA_IdentityService.Service.Services
{
    [ScopedDependency(ServiceType = typeof(IAuthenticateService))]
    public class AuthenticateService(IRefreshTokenRepository repository, IIdentityService identityService, ICacheLayer<RefreshToken> cacheLayer) : BaseService.Service, IAuthenticateService
    {

        private readonly FirebaseAuth _firebaseAuth = FirebaseAuth.DefaultInstance;


        public async Task<AuthenticateResponse?> AuthenticateAsync(AuthenticateModel model, CancellationToken cancellationToken = default)
        {
            if (model.IdToken == null)
            {
                throw new Exception("IdToken credentialis null or empty");
            }

            FirebaseToken? decodedToken = null;
            string uid = string.Empty;
            UserRecord? userRecord = null;
            ApplicationUser? applicationUser = null;
            if (!model.IdToken.IsNullOrEmpty())
            {
                decodedToken = await _firebaseAuth.VerifyIdTokenAsync(model.IdToken);
                if (decodedToken != null)
                {
                    uid = decodedToken.Uid;
                    userRecord = await _firebaseAuth.GetUserAsync(uid);
                }
            }

            if (userRecord != null)
            {
                applicationUser = await identityService.GetUserByUserNameAsync(userRecord.Email);
                if (applicationUser == null)
                {
                    var result = await identityService.CreateUserAsync(userRecord.Email, CoreHelper.CreateRandomPassword(20));
                    applicationUser = await identityService.GetUserByIdAsync(result.UserId);
                    if (applicationUser == null)
                    {
                        throw new Exception("Unable to create user");
                    }
                    if (!userRecord.PhoneNumber.IsNullOrEmpty())
                    {
                        await identityService.SetVerifiedPhoneNumberAsync(applicationUser, userRecord.PhoneNumber);
                    }
                    if (!userRecord.DisplayName.IsNullOrEmpty())
                    {
                        await identityService.SetUserFullNameAsync(applicationUser, userRecord.DisplayName);
                    }
                    if (!userRecord.PhotoUrl.IsNullOrEmpty())
                    {
                        await identityService.SetUserAvatarUrlAsync(applicationUser, userRecord.PhotoUrl);
                    }
                }

                if (applicationUser != null)
                {
                    var roles = await identityService.GetRolesAsync(applicationUser);
                    RefreshToken? device = null;
                    if (model.SessionId != null)
                    {
                        device = await repository.GetSingleAsync(x => x.SessionId == model.SessionId && x.ApplicationUserID == applicationUser.Id, cancellationToken);
                    }
                    RefreshToken? refreshToken = GenerateRefreshToken(applicationUser.Id, model);
                    if (device == null && refreshToken != null)
                    {
                        _ = repository.AddAsync(refreshToken, cancellationToken);
                        return new AuthenticateResponse
                        {
                            AccessToken = await GenerateAccessToken(applicationUser.Id),
                            RefreshToken = refreshToken.Token,
                            User = applicationUser,
                            Roles = roles?.ToArray() ?? []
                        };
                    }
                    else if (device != null && refreshToken != null)
                    {
                        device.PreviousToken = device.Token;
                        device.Token = refreshToken.Token;
                        device.ExpiredAt = refreshToken.ExpiredAt;
                        _ = repository.UpdateAsync(device, cancellationToken);
                        return new AuthenticateResponse
                        {
                            AccessToken = await GenerateAccessToken(applicationUser.Id),
                            RefreshToken = refreshToken.Token,
                            User = applicationUser,
                            Roles = roles?.ToArray() ?? []
                        };
                    }
                }
            }
            return null;
        }

        public async Task<AuthenticateResponse?> RefreshTokenAsync(AuthenticateModel model, CancellationToken cancellationToken = default)
        {
            if (model.RefreshToken == null)
            {
                throw new Exception("RefreshToken is null or empty");
            }
            var validatedToken = ValidateToken(model.RefreshToken);

            if (validatedToken is JwtSecurityToken jwt)
            {
                if(jwt.Claims.FirstOrDefault(x => x.Type == "token_type")?.Value != "refresh_token")
                {
                    throw new Exception("Token type invalid");
                }
                var userId = jwt.Subject;
                var id = jwt.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Id)?.Value;

                var device = await repository.GetSingleAsync(x => x.Token == model.RefreshToken, cancellationToken);
                if (device != null)
                {
                    var user = await identityService.GetUserByIdAsync(userId);
                    var roles = await identityService.GetRolesAsync(user);
                    var refreshToken = GenerateRefreshToken(userId, model, id);
                    device.PreviousToken = device.Token;
                    device.Token = refreshToken.Token;
                    device.IPAdress = model.IPAdress ?? "Unknow IP";
                    device.ExpiredAt = refreshToken.ExpiredAt;
                    _ = repository.UpdateAsync(device, cancellationToken);
                    return new AuthenticateResponse
                    {
                        AccessToken = await GenerateAccessToken(userId),
                        RefreshToken = refreshToken.Token,
                        User = user,
                        Roles = roles?.ToArray() ?? []
                    };
                }
                else
                {
                    throw new Exception("Refresh token not found or expired");
                }
            }
            return null;
        }

        public async Task SignOutAllAsync(string refreshToken, CancellationToken cancellationToken = default)
        {

            var token = ValidateToken(refreshToken);
            if (token != null)
            { 
                var rf = await repository.GetSingleAsync(x => x.Token == refreshToken, cancellationToken);
                if(rf != null)
                {
                    _ = repository.DeleteAsync(x => x.ApplicationUserID == rf.ApplicationUserID, cancellationToken);
                    return;
                }
            }
            throw new Exception("Refresh token not found or expired");
        }

        public Task SignOutAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            _ = repository.DeleteAsync(x => x.Token == refreshToken, cancellationToken);
            return Task.CompletedTask;
        }

        private static RefreshToken GenerateRefreshToken(string userId, AuthenticateModel model, string? refreshTokenId = null)
        {
            var refreshToken = new RefreshToken
            {
                ExpiredAt = DateTime.UtcNow.AddDays(60),
                ApplicationUserID = userId,
                CreatedAt = DateTime.UtcNow,
                IPAdress = model.IPAdress ?? "Unknow IP",
                Browser = model.Browser ?? "Unknow browser",
                OS = model.OS ?? "Unknow OS",
                SessionId = model.SessionId ?? string.Empty,
            };

            var claims = new List<Claim>
            {
                new (JwtClaimTypes.JwtId, Guid.NewGuid().ToString()),
                new (JwtClaimTypes.Id, refreshTokenId ?? refreshToken.Id),
                new (JwtClaimTypes.Issuer, "ISSA"),
                new (JwtClaimTypes.SessionId, refreshToken.SessionId),
                new ("token_type", "refresh_token"),
                new (JwtClaimTypes.Subject, userId),
                new (JwtClaimTypes.IssuedAt, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new (JwtClaimTypes.NotBefore, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new (JwtClaimTypes.Expiration, DateTime.UtcNow.AddDays(60).ToString(CultureInfo.InvariantCulture)),
            };

            var signingCredentials = new SigningCredentials(SystemSettingModel.RSAPrivateKey, SecurityAlgorithms.RsaSha512);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: SystemSettingModel.Configs["Jwt:ValidIssuer"],
                audience: SystemSettingModel.Configs["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                signingCredentials: signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            refreshToken.Token = token;

            return refreshToken;
        }

        private async Task<string> GenerateAccessToken(string userId)
        {
            var user = await identityService.GetUserByIdAsync(userId);
            var userClaims = await identityService.GetClaimsAsync(user);
            var userRoles = await identityService.GetRolesAsync(user);
            var userClaimsAndRoleClaims = userClaims.ToList();
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    userClaimsAndRoleClaims.Add(new Claim(JwtClaimTypes.Role, role));
                }
            }

            var claims = new List<Claim>
            {
                new (JwtClaimTypes.JwtId, Guid.NewGuid().ToString()),
                new (JwtClaimTypes.Issuer, "ISSA"),
                new ("token_type", "access_token"),
                new (JwtClaimTypes.Subject, userId),
                new (JwtClaimTypes.IssuedAt, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new (JwtClaimTypes.NotBefore, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new (JwtClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(60).ToString(CultureInfo.InvariantCulture)),
            };
            claims.AddRange(userClaimsAndRoleClaims);
            var signingCredentials = new SigningCredentials(SystemSettingModel.RSAPrivateKey, SecurityAlgorithms.RsaSha512);
            var jwtSecurityToken = new JwtSecurityToken(
                               issuer: SystemSettingModel.Configs["Jwt:ValidIssuer"],
                               audience: SystemSettingModel.Configs["Jwt:ValidAudience"],
                               claims: claims,
                               expires: DateTime.UtcNow.AddMinutes(60),
                               signingCredentials: signingCredentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }

        private JwtSecurityToken? ValidateToken(string token)
        {
            _ = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = SystemSettingModel.Configs["Jwt:ValidIssuer"],
                ValidAudience = SystemSettingModel.Configs["Jwt:ValidAudience"],
                IssuerSigningKey = SystemSettingModel.RSAPublicKey
            }, out SecurityToken validatedToken);

            if (validatedToken is JwtSecurityToken jwt)
            {
                return jwt;
            }
            return null;
        }
    }
}