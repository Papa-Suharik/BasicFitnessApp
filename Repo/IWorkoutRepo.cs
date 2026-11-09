using BasicFitnessApp.Models;

namespace BasicFitnessApp.Repo;

public interface IWorkoutRepo
{
    Task<UserProfile?> GetUserProfileById(Guid id, CancellationToken cancellationToken);
    Task AddWorkoutAsync(Workout workout, CancellationToken cancellationToken);
    Task SaveChangesAsync();
}