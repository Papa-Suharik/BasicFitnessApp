using BasicFitnessApp.Models;

namespace BasicFitnessApp.Extensions;

public interface IGlobalTokenHandler
{
    string GenerateJwt(User user);
    RefreshToken GenerateRefreshToken(string rawTokenValue, Guid userId);
    string GenerateRawValue();
}