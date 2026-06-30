using BasicFitnessApp.Data;
using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicFitnessApp.Repo;

public class WorkoutRepo(ApplicationContext context) : IWorkoutRepo
{
    public async Task<UserProfile?> GetUserProfileById(Guid id)
    {
        return await context.UserProfiles.Include(x => x.Workouts.OrderByDescending(w => w.DateTime).Take(1)).FirstOrDefaultAsync(x => x.UserId == id);
    }
    public async Task AddWorkoutAsync(Workout workout)
    {
        await context.Workouts.AddAsync(workout);
    }
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}  