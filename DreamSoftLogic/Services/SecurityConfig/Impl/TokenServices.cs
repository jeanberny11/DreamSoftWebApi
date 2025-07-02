using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AutoMapper;
using DreamSoftData.Entities.Public;
using DreamSoftData.Repositories.Public.Interface;
using DreamSoftLogic.Exceptions.Security;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftModel.Models.Public;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DreamSoftLogic.Services.SecurityConfig.Impl;

public class TokenServices(
    IOptions<JwtSetting> jwtOptions,
    IUsersRepository usersRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IMapper mapper,
    IRoleOptionsRepository roleOptionsRepository,
    IPasswordHasher<Users> passwordHasher) : ITokenServices
{
    private readonly JwtSetting _jwtSetting = jwtOptions.Value;

    public async Task<TokenResponse> GetAccessToken(string username, string password, string ipaddress)
    {
        var user = await usersRepository.GetByUsername(username) ??
                   throw new UnAuthorizedUserException("No user was found with that username.");
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            throw new UnAuthorizedUserException("Invalid password.");

        var roleOptions = await roleOptionsRepository.GetRolePermittedOptionsAsync(user.RoleId);
        try
        {
            var token = GenerateAccessToken(mapper.Map<User>(user), mapper.Map<List<RoleOption>>(roleOptions.ToList()));

            //revoke any existing refresh tokens for the user
            await refreshTokenRepository.RevokeUserRefreshTokens(user.UserId, ipaddress);

            var refreshToken = GenerateRefreshToken();
            //check if refresh token already exists for the user
            while (await refreshTokenRepository.CheckTokenExistence(refreshToken))
                refreshToken = GenerateRefreshToken();

            //saving the refresh token in the database
            var refreshTokenEntity = new RefreshTokens
            {
                UserId = user.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = ipaddress,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSetting.RefreshTokenExpirationDays),
                Token = refreshToken
            };
            await refreshTokenRepository.CreateAsync(refreshTokenEntity);
            //return the token response
            return new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                User = mapper.Map<User>(user)
            };
        }
        catch (Exception ex)
        {
            throw new TokenGenerationException(ex.Message);
        }
    }

    public async Task<TokenResponse> GetRefreshToken(string refreshToken, string ipaddress)
    {
        var refreshTokenEntity = await refreshTokenRepository.GetRefreshTokenByToken(refreshToken) ??
                                 throw new UnAuthorizedUserException("Invalid or expired refresh token.");
        if (refreshTokenEntity.RevokedAt is not null)
            throw new UnAuthorizedUserException("Refresh token has already been revoked.");
        if (refreshTokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            refreshTokenEntity.RevokedAt = DateTime.UtcNow;
            refreshTokenEntity.RevokedByIp = ipaddress;
            await refreshTokenRepository.UpdateAsync(refreshTokenEntity);
            throw new UnAuthorizedUserException("Refresh token has expired.");
        }

        var user = await usersRepository.GetByIdAsync(refreshTokenEntity.UserId) ??
                   throw new UnAuthorizedUserException("No user was found with that refresh token.");
        var roleOptions = await roleOptionsRepository.GetRolePermittedOptionsAsync(user.RoleId);
        try
        {
            var token = GenerateAccessToken(mapper.Map<User>(user), mapper.Map<List<RoleOption>>(roleOptions.ToList()));
            //return the token response
            return new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                User = mapper.Map<User>(user)
            };
        }
        catch (Exception ex)
        {
            throw new TokenGenerationException(ex.Message);
        }
    }

    private string GenerateAccessToken(User user, List<RoleOption> roleOptions)
    {
        try
        {
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, _jwtSetting.Subject),
                //new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new(ClaimTypes.Role, JsonSerializer.Serialize(user.Role)),
                new("permissions", JsonSerializer.Serialize(roleOptions))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Audience,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSetting.AccessTokenExpirationMinutes)),
                claims: authClaims,
                signingCredentials: credentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
        catch (Exception ex)
        {
            throw new TokenGenerationException(ex.Message);
        }
    }

    private string GenerateRefreshToken()
    {
        //create the refresh token
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var refreshToken = Convert.ToBase64String(randomBytes);
        return refreshToken;
    }
}