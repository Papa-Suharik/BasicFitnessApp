using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Services;

public interface IWorkoutService
{
    Task<WorkoutForResponse> CreateWorkout(CreateWorkout dto,CancellationToken cancellationToken);
    GroupsOfMuscle SetTargetMuscles(TypeOfWorkout type, Workout? lastWorkout = null);
}