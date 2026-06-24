namespace BasicFitnessApp.Extensions;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BasicFitnessApp.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

public class GlobalTokenHandler(IConfiguration configuration) : IGlobalTokenHandler
{
    public string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity
            (
                [

                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<double>("Jwt:ExpirationMinutes")),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        };

        var handler = new JsonWebTokenHandler();

        var key = handler.CreateToken(tokenDescriptor);

        return key;
    }

    public RefreshToken GenerateRefreshToken(string rawTokenValue, Guid userId)
    {
        string hashedValue = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawTokenValue)));

        var refreshToken = new RefreshToken
        {
            Id = Guid.CreateVersion7(),
            UserId = userId,
            HashedValue = hashedValue,
            CreatedAt = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddDays(15),
        };

        return refreshToken;
    }
    public string GenerateRawValue()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
    }
}