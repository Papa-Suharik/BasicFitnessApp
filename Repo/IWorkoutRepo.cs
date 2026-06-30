using BasicFitnessApp.Models;

namespace BasicFitnessApp.Repo;

public interface IWorkoutRepo
{
    Task<UserProfile?> GetUserProfileById(Guid id);
    Task AddWorkoutAsync(Workout workout);
    Task SaveChangesAsync();
}