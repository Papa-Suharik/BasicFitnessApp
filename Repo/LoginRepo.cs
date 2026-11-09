using BasicFitnessApp.Data;
using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicFitnessApp.Repo;

public class LoginRepo(ApplicationContext context) : ILoginRepo
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await context.RefreshTokens.AddAsync(refreshToken);
    }
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetTokenAsync(string hashedValue)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(x => x.HashedValue == hashedValue);
    }
    public async Task RevokeSessionAsync(Guid userId)
    {
        await context.RefreshTokens.Where(x => x.UserId == userId).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsRevoked, true).SetProperty(p => p.RevokedAt, DateTime.UtcNow));
    }
}