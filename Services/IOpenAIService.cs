using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Services;

public interface IOpenAIService
{
    Task<GeneratedWorkoutDto?> GenerateWorkoutAsync(UserProfile userProfile, GroupsOfMuscle targetMuscles, TypeOfWorkout typeOfWorkout);
}