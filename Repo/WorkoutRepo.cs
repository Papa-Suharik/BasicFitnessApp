using BasicFitnessApp.Data;
using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicFitnessApp.Repo;

public class WorkoutRepo(ApplicationContext context) : IWorkoutRepo
{
    public async Task<UserProfile?> GetUserProfileById(Guid id, CancellationToken cancellationToken)
    {
        return await context.UserProfiles.Include(x => x.Workouts.OrderByDescending(w => w.DateTime).Take(1)).FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);
    }
    public async Task AddWorkoutAsync(Workout workout, CancellationToken cancellationToken)
    {
        await context.Workouts.AddAsync(workout, cancellationToken);
    }
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}  