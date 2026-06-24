using BasicFitnessApp.Data;
using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicFitnessApp.Repo;

public class WorkoutRepo(ApplicationContext context) : IWorkoutRepo
{
    public async Task<UserProfile?> GetUserWithProfileById(Guid id)
    {
        return await context.UserProfiles.Include(x => x.Workouts.OrderByDescending(w => w.DateTime).Take(5)).FirstOrDefaultAsync(x => x.UserId == id);
    }
}  