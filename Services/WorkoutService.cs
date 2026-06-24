using System.Xml;
using BasicFitnessApp.CustomExceptionHandler;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Repo;

namespace BasicFitnessApp.Services;
public class WorkoutService(IWorkoutRepo repo) : IWorkoutService
{
    public async Task CreateWorkout(CreateWorkoutDto dto)
    {
        var userProfile = await repo.GetUserWithProfileById(dto.UserId) ?? throw new NoUserProfileException($"No profile found for user: {dto.UserId}");

        
    }
}