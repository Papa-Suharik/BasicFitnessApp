using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Services;

public interface IOpenAIService
{
    Task<GeneratedWorkout?> GenerateWorkoutAsync(UserProfile userProfile, GroupsOfMuscle targetMuscles, TypeOfWorkout typeOfWorkout, CancellationToken cancellationToken);
}