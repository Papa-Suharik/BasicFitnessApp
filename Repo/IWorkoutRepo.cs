using BasicFitnessApp.Models;

namespace BasicFitnessApp.Repo;

public interface IWorkoutRepo
{
    Task<UserProfile?> GetUserWithProfileById(Guid id);
}