using BasicFitnessApp.Data;
using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicFitnessApp.Repo;
public class UserRepo(ApplicationContext context) : IUserRepo
{
    public async Task<User?>GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
    public async Task<User?>GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<User?>GetByIdWithProfileAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);

        return user;
    } 
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}