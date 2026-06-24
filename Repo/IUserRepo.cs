using BasicFitnessApp.Models;

namespace BasicFitnessApp.Repo;

public interface IUserRepo
{
    Task<User?>GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?>GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?>GetByIdWithProfileAsync(Guid id, CancellationToken cancellationToken);
    Task<User> AddAsync(User user, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}