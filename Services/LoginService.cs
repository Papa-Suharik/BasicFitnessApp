using System.Security.Cryptography;
using System.Text;
using BasicFitnessApp.CustomExceptionHandler;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Extensions;
using BasicFitnessApp.Models;
using BasicFitnessApp.Repo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace BasicFitnessApp.Services;

public class LoginService(ILoginRepo repo, IGlobalTokenHandler handler, IPasswordHasher<User> hasher, ILogger<LoginService> logger) : ILoginService
{
    public async Task<LoginResultDto> Login(LoginDto dto)
    {
        var user = await repo.GetUserByEmailAsync(dto.Email) ?? throw new UserNotFoundExcpetion($"No user found under {dto.Email} email!");

        var result = hasher.VerifyHashedPassword(user, user.Password, dto.Password);

        if(result == PasswordVerificationResult.Failed)
        {
            throw new AuthenticationFailedException("The credentials entered don't match our records. Please try again or reset your password.");
        }

        string refreshTokenRaw = handler.GenerateRawValue();

        var refreshToken = handler.GenerateRefreshToken(refreshTokenRaw, user.Id);

        await repo.AddRefreshToken(refreshToken);

        await repo.SaveChangesAsync();

        return new LoginResultDto
        {
            JwtToken = handler.GenerateJwt(user),
            RefreshToken = refreshTokenRaw,
            Id = user.Id
        };
    }
    public async Task<LoginResultDto> RefreshTokens(RefreshDto dto)
    {
        var user = await repo.GetUserByIdAsync(dto.Id) ?? throw new UserNotFoundExcpetion($"No user found under {dto.Id} Id!");

        var oldRefreshToken = await VerifyToken(dto.RawToken, dto.Id);

        string refreshTokenRaw = handler.GenerateRawValue();

        var newRefreshToken = handler.GenerateRefreshToken(refreshTokenRaw, dto.Id);

        oldRefreshToken.Revoke(newRefreshToken.Id);

        await repo.AddRefreshToken(newRefreshToken);
    
        await repo.SaveChangesAsync();

        return new LoginResultDto
        {
            JwtToken = handler.GenerateJwt(user),
            RefreshToken = refreshTokenRaw,
            Id = dto.Id
        };
    }

    public async Task<RefreshToken> VerifyToken(string rawTokenValue, Guid userId)
    {
        string hashedValue = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawTokenValue)));

        var refreshToken = await repo.GetTokenAsync(hashedValue);

        if(refreshToken == null )
        {
            logger.LogWarning("Uknown refresh token provided. Potential forgery attack");
            await repo.RevokeSessionAsync(userId);
            throw new SecurityException("Invalid Token"); 
        }
            
        if(refreshToken.IsRevoked || refreshToken.ReplacedWithTokenId != null)
        {
            logger.LogCritical($"Security Breach: Revoked Refresh Token provided for {userId}", refreshToken.Id);
            await repo.RevokeSessionAsync(userId);
            throw new SecurityException("Token has already been consumed");
        }

        if(refreshToken.ExpiresOn <= DateTime.UtcNow)
        {
            logger.LogWarning($"Token expired naturally for {userId}", refreshToken.Id);
            throw new SecurityException("Provided token has expired");
        }

        return refreshToken;
    }
}