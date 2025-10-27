using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftData.Repositories.Menu.Interfaces;
using DreamSoftLogic.Exceptions.Security;
using DreamSoftLogic.Services.SecurityConfig.Interface;
using DreamSoftModel.Models.Authentication;
using DreamSoftModel.Models.Menu;
using DreamSoftModel.Models.SecurityConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DreamSoftLogic.Services.SecurityConfig.Impl;

public class LoginServices(
    IOptions<JwtSetting> jwtOptions,
    IUsersRepository usersRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IMapper mapper,
    IRoleOptionsRepository roleOptionsRepository,
    IAccountRepository accountRepository,
    IPasswordHasher<Users> passwordHasher,
    ILogger<LoginServices> logger) : ILoginServices
{
    private readonly JwtSetting _jwtSetting = jwtOptions.Value;

    public async Task<LoginResponse> LoginAsync(LoginRequest request, string ipaddress)
    {
        const string genericError = "Invalid email, username, or password.";

        var account = await accountRepository.GetAccountByEmail(request.Email);
        if (account == null)
        {
            logger.LogWarning("Failed login attempt - Account not found. Email: {Email}, IP: {IpAddress}", request.Email, ipaddress);
            throw new UnAuthorizedUserException(genericError);
        }

        var user = await usersRepository.GetAuthUser(account.AccountId, request.UserName);
        if (user == null)
        {
            logger.LogWarning("Failed login attempt - User not found. Email: {Email}, Username: {Username}, IP: {IpAddress}",
                request.Email, request.UserName, ipaddress);
            throw new UnAuthorizedUserException(genericError);
        }

        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            logger.LogWarning("Failed login attempt - Invalid password. Email: {Email}, Username: {Username}, UserId: {UserId}, IP: {IpAddress}",
                request.Email, request.UserName, user.UserId, ipaddress);
            throw new UnAuthorizedUserException(genericError);
        }

        logger.LogInformation("Successful login. UserId: {UserId}, Username: {Username}, IP: {IpAddress}",
            user.UserId, user.UserName, ipaddress);

        var roleOptions = await roleOptionsRepository.GetRolePermittedOptionsAsync(user.RoleId);
        try
        {
            var token = GenerateAccessToken(mapper.Map<User>(user), mapper.Map<List<RoleOption>>(roleOptions.ToList()));
            //return the token response
            return new LoginResponse
            {
                AccessToken = token,
                User = mapper.Map<User>(user)
            };
        }
        catch (Exception ex)
        {
            throw new TokenGenerationException(ex.Message);
        }
    }

    public async Task<LoginResponse> GetRefreshToken(string refreshToken, string ipaddress)
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
            return new LoginResponse
            {
                AccessToken = token,
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
                new(ClaimTypes.Name, user.FirstName),
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

    public async Task<string> CreateRefreshToken(int userid, string ipaddress)
    {
        //revoke any existing refresh tokens for the user
        await refreshTokenRepository.RevokeUserRefreshTokens(userid, ipaddress);

        var refreshToken = GenerateRefreshToken();
        //check if refresh token already exists for the user
        while (await refreshTokenRepository.CheckTokenExistence(refreshToken))
            refreshToken = GenerateRefreshToken();

        //saving the refresh token in the database
        var refreshTokenEntity = new RefreshTokens
        {
            UserId = userid,
            CreatedAt = DateTime.UtcNow,
            CreatedByIp = ipaddress,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSetting.RefreshTokenExpirationDays),
            Token = refreshToken
        };
        await refreshTokenRepository.CreateAsync(refreshTokenEntity);
        return refreshToken;
    }

    private static string GenerateRefreshToken()
    {
        //create the refresh token
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var refreshToken = Convert.ToBase64String(randomBytes);
        return refreshToken;
    }

    public async Task LogoutAsync(string refreshToken, string ipaddress)
    {
        var refreshTokenEntity = await refreshTokenRepository.GetRefreshTokenByToken(refreshToken) ??
                                 throw new UnAuthorizedUserException("Invalid or expired refresh token.");
        refreshTokenEntity.ExpiresAt = DateTime.UtcNow;
        refreshTokenEntity.RevokedAt = DateTime.UtcNow;
        refreshTokenEntity.RevokedByIp = ipaddress;
        await refreshTokenRepository.UpdateAsync(refreshTokenEntity);
    }
}