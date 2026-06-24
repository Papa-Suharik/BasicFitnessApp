using BasicFitnessApp.Models;

namespace BasicFitnessApp.Repo;

public interface ILoginRepo
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task SaveChangesAsync();
    Task<RefreshToken?> GetTokenAsync(string hashedValue);
    Task RevokeSessionAsync(Guid userId);
}